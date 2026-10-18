-- Mevcut satirlar ve saat bilgisi korunur; NULL veya saat iceren Tarih DATE yapilmaz.
-- Bilinmeyen semada uygulanamayan adimlar PRINT ile bildirilerek atlanir.
SET XACT_ABORT OFF;
GO
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.Antrenmanlar', N'U') AND name = N'SporcuId')
AND NOT EXISTS (
 SELECT 1 FROM sys.indexes i JOIN sys.index_columns ic ON ic.object_id=i.object_id AND ic.index_id=i.index_id
 JOIN sys.columns c ON c.object_id=ic.object_id AND c.column_id=ic.column_id
 WHERE i.object_id=OBJECT_ID(N'dbo.Antrenmanlar') AND c.name=N'SporcuId' AND ic.key_ordinal=1
 AND i.is_disabled=0 AND i.has_filter=0)
AND NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.Antrenmanlar') AND name=N'IX_Antrenmanlar_SporcuId')
BEGIN TRY
 EXEC(N'CREATE INDEX IX_Antrenmanlar_SporcuId ON dbo.Antrenmanlar (SporcuId);');
END TRY
BEGIN CATCH
 PRINT N'Antrenmanlar index atlandi: ' + ERROR_MESSAGE();
END CATCH;
GO
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.Takipler', N'U') AND name = N'SporcuId')
AND NOT EXISTS (
 SELECT 1 FROM sys.indexes i JOIN sys.index_columns ic ON ic.object_id=i.object_id AND ic.index_id=i.index_id
 JOIN sys.columns c ON c.object_id=ic.object_id AND c.column_id=ic.column_id
 WHERE i.object_id=OBJECT_ID(N'dbo.Takipler') AND c.name=N'SporcuId' AND ic.key_ordinal=1
 AND i.is_disabled=0 AND i.has_filter=0)
AND NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.Takipler') AND name=N'IX_Takipler_SporcuId')
BEGIN TRY
 EXEC(N'CREATE INDEX IX_Takipler_SporcuId ON dbo.Takipler (SporcuId);');
END TRY
BEGIN CATCH
 PRINT N'Takipler index atlandi: ' + ERROR_MESSAGE();
END CATCH;
GO
IF EXISTS (SELECT 1 FROM sys.tables WHERE object_id=OBJECT_ID(N'dbo.Antrenmanlar'))
AND NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.Antrenmanlar') AND name=N'Gun')
 ALTER TABLE dbo.Antrenmanlar ADD Gun TINYINT NULL;
GO
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.Antrenmanlar') AND name=N'Gun')
AND NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id=OBJECT_ID(N'dbo.Antrenmanlar') AND name=N'CK_Antrenmanlar_Gun')
BEGIN TRY
 EXEC(N'ALTER TABLE dbo.Antrenmanlar WITH NOCHECK ADD CONSTRAINT CK_Antrenmanlar_Gun CHECK (Gun IS NULL OR Gun BETWEEN 1 AND 7);');
END TRY
BEGIN CATCH
 PRINT N'Gun CHECK atlandi: ' + ERROR_MESSAGE();
END CATCH;
GO
IF EXISTS (SELECT 1 FROM sys.tables WHERE object_id=OBJECT_ID(N'dbo.Antrenmanlar'))
AND NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.Antrenmanlar') AND name=N'OlusturmaTarihi')
 ALTER TABLE dbo.Antrenmanlar ADD OlusturmaTarihi DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME() WITH VALUES;
GO
-- Uzunluk, Unicode ve collation korunur; tahmini NVARCHAR(100) dayatilmaz.
-- COLLATE koseli parantezli ad kabul etmez (QUOTENAME sozdizimi hatasi verir); ad yalnizca harf/rakam/alt cizgi ise kullanilir.
DECLARE @Type nvarchar(500), @Sql nvarchar(max);
SELECT @Type = QUOTENAME(t.name) +
 CASE WHEN t.name IN (N'nvarchar',N'nchar',N'varchar',N'char',N'varbinary',N'binary')
 THEN N'(' + CASE WHEN c.max_length=-1 THEN N'MAX' ELSE CONVERT(nvarchar(10),c.max_length / CASE WHEN t.name IN (N'nvarchar',N'nchar') THEN 2 ELSE 1 END) END + N')'
 ELSE N'' END + CASE WHEN c.collation_name IS NOT NULL THEN N' COLLATE ' + c.collation_name ELSE N'' END
FROM sys.columns c JOIN sys.types t ON t.user_type_id=c.user_type_id
WHERE c.object_id=OBJECT_ID(N'dbo.Sporcular') AND c.name=N'Ad' AND c.is_nullable=1
 AND c.is_computed=0 AND t.is_user_defined=0 AND t.name IN (N'nvarchar',N'nchar',N'varchar',N'char')
 AND (c.collation_name IS NULL OR c.collation_name NOT LIKE N'%[^A-Za-z0-9_]%');
IF @Type IS NULL PRINT N'Ad NOT NULL atlandi: kolon zaten NOT NULL veya desteklenen degistirilebilir metin turunde degil.';
IF @Type IS NOT NULL
BEGIN TRY
 SET @Sql=N'IF NOT EXISTS (SELECT 1 FROM dbo.Sporcular WHERE Ad IS NULL) ALTER TABLE dbo.Sporcular ALTER COLUMN Ad ' + @Type + N' NOT NULL; ELSE PRINT N''Ad NULL satirlar nedeniyle degistirilmedi.'';';
 EXEC sys.sp_executesql @Sql;
END TRY
BEGIN CATCH
 PRINT N'Ad NOT NULL atlandi: ' + ERROR_MESSAGE();
END CATCH;
GO
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.Sporcular') AND name=N'Ad')
AND NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id=OBJECT_ID(N'dbo.Sporcular') AND name=N'CK_Sporcular_Ad_Dolu')
BEGIN TRY
 EXEC(N'ALTER TABLE dbo.Sporcular WITH NOCHECK ADD CONSTRAINT CK_Sporcular_Ad_Dolu CHECK (Ad IS NOT NULL AND LEN(LTRIM(RTRIM(CONVERT(nvarchar(max), Ad)))) > 0);');
END TRY
BEGIN CATCH
 PRINT N'Ad CHECK atlandi: ' + ERROR_MESSAGE();
END CATCH;
GO
-- Uzunluk, Unicode ve collation korunur; tahmini NVARCHAR(100) dayatilmaz.
-- COLLATE koseli parantezli ad kabul etmez (QUOTENAME sozdizimi hatasi verir); ad yalnizca harf/rakam/alt cizgi ise kullanilir.
DECLARE @Type nvarchar(500), @Sql nvarchar(max);
SELECT @Type = QUOTENAME(t.name) +
 CASE WHEN t.name IN (N'nvarchar',N'nchar',N'varchar',N'char',N'varbinary',N'binary')
 THEN N'(' + CASE WHEN c.max_length=-1 THEN N'MAX' ELSE CONVERT(nvarchar(10),c.max_length / CASE WHEN t.name IN (N'nvarchar',N'nchar') THEN 2 ELSE 1 END) END + N')'
 ELSE N'' END + CASE WHEN c.collation_name IS NOT NULL THEN N' COLLATE ' + c.collation_name ELSE N'' END
FROM sys.columns c JOIN sys.types t ON t.user_type_id=c.user_type_id
WHERE c.object_id=OBJECT_ID(N'dbo.Sporcular') AND c.name=N'SporDali' AND c.is_nullable=1
 AND c.is_computed=0 AND t.is_user_defined=0 AND t.name IN (N'nvarchar',N'nchar',N'varchar',N'char')
 AND (c.collation_name IS NULL OR c.collation_name NOT LIKE N'%[^A-Za-z0-9_]%');
IF @Type IS NULL PRINT N'SporDali NOT NULL atlandi: kolon zaten NOT NULL veya desteklenen degistirilebilir metin turunde degil.';
IF @Type IS NOT NULL
BEGIN TRY
 SET @Sql=N'IF NOT EXISTS (SELECT 1 FROM dbo.Sporcular WHERE SporDali IS NULL) ALTER TABLE dbo.Sporcular ALTER COLUMN SporDali ' + @Type + N' NOT NULL; ELSE PRINT N''SporDali NULL satirlar nedeniyle degistirilmedi.'';';
 EXEC sys.sp_executesql @Sql;
END TRY
BEGIN CATCH
 PRINT N'SporDali NOT NULL atlandi: ' + ERROR_MESSAGE();
END CATCH;
GO
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.Sporcular') AND name=N'SporDali')
AND NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id=OBJECT_ID(N'dbo.Sporcular') AND name=N'CK_Sporcular_SporDali_Dolu')
BEGIN TRY
 EXEC(N'ALTER TABLE dbo.Sporcular WITH NOCHECK ADD CONSTRAINT CK_Sporcular_SporDali_Dolu CHECK (SporDali IS NOT NULL AND LEN(LTRIM(RTRIM(CONVERT(nvarchar(max), SporDali)))) > 0);');
END TRY
BEGIN CATCH
 PRINT N'SporDali CHECK atlandi: ' + ERROR_MESSAGE();
END CATCH;
GO
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.Takipler') AND name=N'Tarih' AND system_type_id<>TYPE_ID(N'date') AND is_computed=0)
BEGIN TRY
 EXEC(N'IF EXISTS (SELECT 1 FROM dbo.Takipler WHERE Tarih IS NULL)
 PRINT N''Tarih NULL satirlar nedeniyle degistirilmedi.'';
 ELSE IF EXISTS (SELECT 1 FROM dbo.Takipler WHERE CAST(Tarih AS TIME) <> ''00:00:00'')
 PRINT N''Tarih saat bileseni iceren satirlar nedeniyle degistirilmedi.'';
 ELSE ALTER TABLE dbo.Takipler ALTER COLUMN Tarih DATE NOT NULL;');
END TRY
BEGIN CATCH
 PRINT N'Tarih donusumu atlandi: ' + ERROR_MESSAGE();
END CATCH;
GO
-- Gercek SporTakip.bak semasinda Tarih zaten DATE NULL; yalnizca NOT NULL uygulanir.
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.Takipler') AND name=N'Tarih' AND system_type_id=TYPE_ID(N'date') AND is_nullable=1 AND is_computed=0)
BEGIN TRY
 EXEC(N'IF EXISTS (SELECT 1 FROM dbo.Takipler WHERE Tarih IS NULL)
 PRINT N''Tarih NULL satirlar nedeniyle degistirilmedi.'';
 ELSE ALTER TABLE dbo.Takipler ALTER COLUMN Tarih DATE NOT NULL;');
END TRY
BEGIN CATCH
 PRINT N'Tarih NOT NULL atlandi: ' + ERROR_MESSAGE();
END CATCH;
GO
SET XACT_ABORT ON;
GO
