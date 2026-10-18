-- README.md örnek verileri (idempotent). Önce 001_initial.sql çalıştırılmalı.
IF NOT EXISTS (SELECT 1 FROM dbo.Sporcular WHERE Ad = N'Ayberk Arda')
    INSERT INTO dbo.Sporcular (Ad, SporDali) VALUES (N'Ayberk Arda', N'Yüzme');

DECLARE @sid INT = (SELECT TOP 1 Id FROM dbo.Sporcular WHERE Ad = N'Ayberk Arda' ORDER BY Id);

IF NOT EXISTS (SELECT 1 FROM dbo.Antrenmanlar WHERE SporcuId = @sid AND AntrenmanAdi = N'Göğüs Günü')
    INSERT INTO dbo.Antrenmanlar (SporcuId, AntrenmanAdi, Detaylar)
    VALUES (@sid, N'Göğüs Günü', N'Chest Press, Incline Dumbbell Press');

IF NOT EXISTS (SELECT 1 FROM dbo.Takipler WHERE SporcuId = @sid AND Aciklama = N'Ağırlık: 95kg, setler tamamlandı')
    INSERT INTO dbo.Takipler (SporcuId, Tarih, Aciklama)
    VALUES (@sid, '2025-01-01', N'Ağırlık: 95kg, setler tamamlandı');
GO
