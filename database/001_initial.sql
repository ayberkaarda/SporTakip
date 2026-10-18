-- SporTakip ilk şema (idempotent). Kaynak: SporTakip.bak restore + verify-schema.sql (SQL Server 2022, uyumluluk 160).
-- DOĞRULANDI (restore edilmiş SporTakip.bak, DB collation Turkish_CI_AS):
--   Sporcular.Id, Antrenmanlar.Id, Takipler.Id        : INT IDENTITY(1,1) NOT NULL, adsız clustered PK (PK__Sporcula__3214EC075BC4A3EF vb.)
--   Sporcular.Ad                                       : NVARCHAR(100) NULL (NOT NULL + CHECK 004_butunluk.sql ile eklenir)
--   Sporcular.SporDali                                 : NVARCHAR(100) NULL
--   Antrenmanlar.SporcuId, Takipler.SporcuId           : INT NULL, adsız FK -> Sporcular(Id), ON DELETE/UPDATE NO ACTION
--                                                        (FK__Antrenman__Sporc__4BAC3F29, FK__Takipler__Sporcu__4E88ABD4; CASCADE 002 ile)
--   Antrenmanlar.AntrenmanAdi                          : NVARCHAR(100) NULL
--   Antrenmanlar.Detaylar                              : NVARCHAR(MAX) NULL
--   Takipler.Tarih                                     : DATE NULL (NOT NULL 004_butunluk.sql ile eklenir)
--   Takipler.Aciklama                                  : NVARCHAR(MAX) NULL
--   Metin kolonları DB varsayılan collation'ını kullanır; ek index, CHECK veya DEFAULT yok.
-- Not: Veritabanı adı SporTakip; CREATE DATABASE bu dosyada yok.

IF OBJECT_ID(N'dbo.Sporcular', N'U') IS NULL
CREATE TABLE dbo.Sporcular (
    Id      INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Ad      NVARCHAR(100) NULL,
    SporDali NVARCHAR(100) NULL
);
GO

IF OBJECT_ID(N'dbo.Antrenmanlar', N'U') IS NULL
CREATE TABLE dbo.Antrenmanlar (
    Id           INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    SporcuId     INT NULL FOREIGN KEY REFERENCES dbo.Sporcular(Id),
    AntrenmanAdi NVARCHAR(100) NULL,
    Detaylar     NVARCHAR(MAX) NULL
);
GO

IF OBJECT_ID(N'dbo.Takipler', N'U') IS NULL
CREATE TABLE dbo.Takipler (
    Id       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    SporcuId INT NULL FOREIGN KEY REFERENCES dbo.Sporcular(Id),
    Tarih    DATE NULL,
    Aciklama NVARCHAR(MAX) NULL
);
GO
