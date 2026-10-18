-- SporTakip ilk şema (idempotent). Kaynak: SporTakip.bak ikili taraması + C# sorguları.
-- KESİN (bak içinden): tablolar Sporcular/Antrenmanlar/Takipler; kolon adları; PK adları
--   (PK__Sporcula__3214EC075BC4A3EF vb. = adsız PK, Id kolonu); FK Antrenmanlar.SporcuId ve Takipler.SporcuId -> Sporcular(Id).
-- DOĞRULANMADI (tahmin; verify-schema.sql ile restore edilmiş DB'de karşılaştırın):
--   Sporcular.Id, Antrenmanlar.Id, Takipler.Id        : INT IDENTITY(1,1) (PK adının 3214EC07 son eki "Id INT IDENTITY PRIMARY KEY" kalıbına işaret eder)
--   Sporcular.Ad                                       : NVARCHAR(100) NOT NULL (uzunluk/null tahmin)
--   Sporcular.SporDali                                 : NVARCHAR(100) NULL      (uzunluk/null tahmin)
--   Antrenmanlar.SporcuId, Takipler.SporcuId           : INT (null durumu tahmin)
--   Antrenmanlar.AntrenmanAdi                          : NVARCHAR(100) (uzunluk/null tahmin)
--   Antrenmanlar.Detaylar                              : NVARCHAR(MAX) (tahmin)
--   Takipler.Tarih                                     : DATETIME (DATE de olabilir) (tahmin)
--   Takipler.Aciklama                                  : NVARCHAR(MAX) (tahmin)
--   Veri UTF-16 saklandığı için metin kolonları NVARCHAR kabul edildi. FK'larda ON DELETE davranışı varsayılan (NO ACTION).
-- Not: Veritabanı adı SporTakip; CREATE DATABASE bu dosyada yok.

IF OBJECT_ID(N'dbo.Sporcular', N'U') IS NULL
CREATE TABLE dbo.Sporcular (
    Id      INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Ad      NVARCHAR(100) NOT NULL,
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
    Tarih    DATETIME NULL,
    Aciklama NVARCHAR(MAX) NULL
);
GO
