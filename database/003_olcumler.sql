-- Yapisal olcumler (idempotent). Onkosul: 001_initial.sql.
IF OBJECT_ID(N'dbo.Olcumler', N'U') IS NULL
CREATE TABLE dbo.Olcumler (
    Id      INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TakipId INT NOT NULL,
    Tur     NVARCHAR(50) NOT NULL,
    Deger   DECIMAL(9,2) NOT NULL,
    Birim   NVARCHAR(20) NULL,
    CONSTRAINT FK_Olcumler_Takipler FOREIGN KEY (TakipId)
        REFERENCES dbo.Takipler(Id) ON DELETE CASCADE
);
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.Olcumler', N'U')
        AND name = N'IX_Olcumler_TakipId'
)
CREATE INDEX IX_Olcumler_TakipId ON dbo.Olcumler (TakipId);
GO
