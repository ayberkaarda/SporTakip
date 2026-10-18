-- SporTakip sporcu yabancı anahtarları: ON DELETE CASCADE (idempotent).
-- FK adları sistem kataloğundan bulunur; zaten CASCADE olanlara dokunulmaz.
-- Mevcut FK adı, ON UPDATE davranışı ve NOT FOR REPLICATION seçeneği korunur.
-- Önkoşul: 001_initial.sql ile oluşturulan tablolar ve yabancı anahtarlar mevcut.

SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @Sql NVARCHAR(MAX) = N'';

    SELECT @Sql = @Sql
        + N'ALTER TABLE [dbo].' + QUOTENAME(t.name)
        + N' DROP CONSTRAINT ' + QUOTENAME(fk.name) + N';' + NCHAR(13) + NCHAR(10)
        + N'ALTER TABLE [dbo].' + QUOTENAME(t.name)
        + N' WITH CHECK ADD CONSTRAINT ' + QUOTENAME(fk.name)
        + N' FOREIGN KEY ([SporcuId]) REFERENCES [dbo].[Sporcular] ([Id])'
        + N' ON DELETE CASCADE ON UPDATE '
        + REPLACE(fk.update_referential_action_desc, N'_', N' ')
        + CASE WHEN fk.is_not_for_replication = 1
            THEN N' NOT FOR REPLICATION' ELSE N'' END
        + N';' + NCHAR(13) + NCHAR(10)
    FROM sys.foreign_keys AS fk
    INNER JOIN sys.tables AS t ON t.object_id = fk.parent_object_id
    INNER JOIN sys.foreign_key_columns AS fkc
        ON fkc.constraint_object_id = fk.object_id
    INNER JOIN sys.columns AS pc
        ON pc.object_id = fkc.parent_object_id
        AND pc.column_id = fkc.parent_column_id
    INNER JOIN sys.columns AS rc
        ON rc.object_id = fkc.referenced_object_id
        AND rc.column_id = fkc.referenced_column_id
    WHERE t.schema_id = SCHEMA_ID(N'dbo')
        AND t.name IN (N'Antrenmanlar', N'Takipler')
        AND pc.name = N'SporcuId'
        AND fk.referenced_object_id = OBJECT_ID(N'dbo.Sporcular', N'U')
        AND rc.name = N'Id'
        AND fk.delete_referential_action <> 1
        AND NOT EXISTS (
            SELECT 1
            FROM sys.foreign_key_columns AS other
            WHERE other.constraint_object_id = fk.object_id
                AND other.constraint_column_id <> fkc.constraint_column_id
        );

    IF @Sql <> N''
        EXEC sys.sp_executesql @Sql;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO
