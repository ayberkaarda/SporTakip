using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SporTakip.Application.Abstractions.Repositories;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Application.Reporting;
using SporTakip.Application.Services;
using SporTakip.Domain;
using Xunit;

namespace SporTakip.Application.Tests;

public class RaporServiceTests
{
    private readonly FakeSporcuRepository _sporcular = new();
    private readonly FakeAntrenmanRepository _antrenmanlar = new();
    private readonly FakeTakipRepository _takipler = new();
    private readonly FakeOlcumRepository _olcumler;
    private readonly RaporService _sut;

    public RaporServiceTests()
    {
        _olcumler = new FakeOlcumRepository(_takipler);
        _sut = new RaporService(_sporcular, _antrenmanlar, _takipler, _olcumler);
    }

    [Fact]
    public async Task SporcuRaporuOlusturAsync_sporcu_yoksa_null_doner()
    {
        (await _sut.SporcuRaporuOlusturAsync(1)).Should().BeNull();
    }

    [Fact]
    public async Task SporcuRaporuOlusturAsync_sporcu_antrenman_ve_takipleri_toplar()
    {
        _sporcular.Items.Add(new Sporcu { Id = 1, Ad = "Ali", SporDali = "Yüzme" });
        _antrenmanlar.Items.Add(new Antrenman { Id = 1, SporcuId = 1, AntrenmanAdi = "Sabah" });
        _antrenmanlar.Items.Add(new Antrenman { Id = 2, SporcuId = 2, AntrenmanAdi = "Başka" });
        _takipler.Items.Add(new Takip { Id = 1, SporcuId = 1, Tarih = new DateTime(2024, 1, 1), Aciklama = "a" });

        var rapor = await _sut.SporcuRaporuOlusturAsync(1);

        rapor!.Sporcu.Ad.Should().Be("Ali");
        rapor.Antrenmanlar.Should().ContainSingle();
        rapor.Takipler.Should().ContainSingle();
    }

    [Fact]
    public void MetneDonustur_baslik_ve_takip_satirlarini_uretir()
    {
        var rapor = new SporcuRaporu(
            new Sporcu { Id = 1, Ad = "Ali", SporDali = "Yüzme" },
            [],
            [
                new Takip { Tarih = new DateTime(2024, 3, 5), Aciklama = "Hız arttı" },
                new Takip { Tarih = new DateTime(2024, 1, 20), Aciklama = "Başlangıç" }
            ]);

        var metin = _sut.MetneDonustur(rapor);

        metin.Replace("\r\n", "\n").Should().Be(
            "=== Ali - Yüzme Raporu ===\n05.03.2024 - Hız arttı\n20.01.2024 - Başlangıç\n");
    }

    [Fact]
    public void MetneDonustur_takip_yoksa_sadece_baslik_doner()
    {
        var rapor = new SporcuRaporu(new Sporcu { Ad = "Ali", SporDali = "Koşu" }, [], []);

        _sut.MetneDonustur(rapor).Trim().Should().Be("=== Ali - Koşu Raporu ===");
    }

    [Fact]
    public void AddSporTakipApplication_dort_servisi_cozer()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ISporcuRepository>(_sporcular);
        services.AddSingleton<IAntrenmanRepository>(_antrenmanlar);
        services.AddSingleton<ITakipRepository>(_takipler);
        services.AddSingleton<IOlcumRepository>(_olcumler);
        services.AddSporTakipApplication();

        using var provider = services.BuildServiceProvider(validateScopes: true);
        using var scope = provider.CreateScope();

        scope.ServiceProvider.GetRequiredService<ISporcuService>().Should().BeOfType<SporcuService>();
        scope.ServiceProvider.GetRequiredService<IAntrenmanService>().Should().BeOfType<AntrenmanService>();
        scope.ServiceProvider.GetRequiredService<ITakipService>().Should().BeOfType<TakipService>();
        scope.ServiceProvider.GetRequiredService<IRaporService>().Should().BeOfType<RaporService>();
    }

    private const string Baslik = "Tarih;Takip Açıklaması;Ölçüm Türü;Değer;Birim\r\n";

    private void Takip(int id, DateTime tarih, string? aciklama)
        => _takipler.Items.Add(new Takip { Id = id, SporcuId = 1, Tarih = tarih, Aciklama = aciklama! });

    private void Olcum(int takipId, string tur, decimal deger, string? birim)
        => _olcumler.Items.Add(new Olcum { Id = _olcumler.Items.Count + 1, TakipId = takipId, Tur = tur, Deger = deger, Birim = birim });

    private async Task<string> Csv()
    {
        _sporcular.Items.Add(new Sporcu { Id = 1, Ad = "Ali", SporDali = "Yüzme" });
        return (await _sut.CsvOlusturAsync(1))!;
    }

    [Fact]
    public async Task CsvOlusturAsync_sporcu_yoksa_null_doner()
        => (await _sut.CsvOlusturAsync(99)).Should().BeNull();

    [Fact]
    public async Task CsvOlusturAsync_takip_yoksa_sadece_baslik_doner()
        => (await Csv()).Should().Be(Baslik);

    [Fact]
    public async Task CsvOlusturAsync_olcumsuz_takip_tek_satir_bos_olcum_sutunlari()
    {
        Takip(1, new DateTime(2024, 3, 5), "Hız arttı");
        (await Csv()).Should().Be(Baslik + "05.03.2024;Hız arttı;;;\r\n");
    }

    [Fact]
    public async Task CsvOlusturAsync_olcumlu_takip_olcum_basina_satir_uretir()
    {
        Takip(1, new DateTime(2024, 3, 5), "Test");
        Olcum(1, "Boy", 180, "cm");
        Olcum(1, "Kilo", 75.5m, "kg");
        Olcum(2, "Başka", 1, "x");

        (await Csv()).Should().Be(Baslik + "05.03.2024;Test;Boy;180;cm\r\n05.03.2024;Test;Kilo;75,5;kg\r\n");
    }

    [Fact]
    public async Task CsvOlusturAsync_birim_null_ise_bos_birakir()
    {
        Takip(1, new DateTime(2024, 3, 5), "T");
        Olcum(1, "Skor", 3, null);
        (await Csv()).Should().Be(Baslik + "05.03.2024;T;Skor;3;\r\n");
    }

    [Theory]
    [InlineData("12.3456", "12,35")]
    [InlineData("1234.5", "1234,5")]
    [InlineData("0.5", "0,5")]
    [InlineData("-2.25", "-2,25")]
    [InlineData("10.00", "10")]
    public async Task CsvOlusturAsync_deger_trTR_en_fazla_iki_ondalik(string deger, string beklenen)
    {
        Takip(1, new DateTime(2024, 1, 1), "T");
        Olcum(1, "Tür", decimal.Parse(deger, System.Globalization.CultureInfo.InvariantCulture), "b");
        (await Csv()).Should().Be(Baslik + $"01.01.2024;T;Tür;{beklenen};b\r\n");
    }

    [Fact]
    public async Task CsvOlusturAsync_negatif_deger_kesme_isaretlenmez()
    {
        Takip(1, new DateTime(2024, 1, 1), "T");
        Olcum(1, "Tür", -5m, "b");
        (await Csv()).Should().Contain(";-5;").And.NotContain("'-5");
    }

    [Fact]
    public async Task CsvOlusturAsync_ozel_karakterli_alanlari_tirnaklar()
    {
        Takip(1, new DateTime(2024, 1, 1), "a;b \"c\"\nd");
        (await Csv()).Should().Be(Baslik + "01.01.2024;\"a;b \"\"c\"\"\nd\";;;\r\n");
    }

    [Fact]
    public async Task CsvOlusturAsync_olcum_turu_ve_birimi_de_kacirilir()
    {
        Takip(1, new DateTime(2024, 1, 1), "T");
        Olcum(1, "A;B", 1, "x\"y");
        (await Csv()).Should().Be(Baslik + "01.01.2024;T;\"A;B\";1;\"x\"\"y\"\r\n");
    }

    [Theory]
    [InlineData("=1+1", "'=1+1")]
    [InlineData("+90", "'+90")]
    [InlineData("-5 tur", "'-5 tur")]
    [InlineData("@SUM(A1)", "'@SUM(A1)")]
    [InlineData("normal=", "normal=")]
    public async Task CsvOlusturAsync_enjeksiyon_korumasi_metin_alanlarinda(string girdi, string beklenen)
    {
        Takip(1, new DateTime(2024, 1, 1), girdi);
        Olcum(1, girdi, 1, girdi);
        (await Csv()).Should().Be(Baslik + $"01.01.2024;{beklenen};{beklenen};1;{beklenen}\r\n");
    }

    [Fact]
    public async Task CsvOlusturAsync_enjeksiyon_oneki_ve_kacis_birlikte()
    {
        Takip(1, new DateTime(2024, 1, 1), "=A;B");
        (await Csv()).Should().Be(Baslik + "01.01.2024;\"'=A;B\";;;\r\n");
    }

    [Fact]
    public async Task CsvOlusturAsync_takipleri_eskiden_yeniye_siralar()
    {
        Takip(1, new DateTime(2024, 3, 1), "Yeni");
        Takip(2, new DateTime(2024, 1, 1), "Eski");
        Takip(3, new DateTime(2024, 2, 1), "Orta");

        (await Csv()).Should().Be(Baslik + "01.01.2024;Eski;;;\r\n01.02.2024;Orta;;;\r\n01.03.2024;Yeni;;;\r\n");
    }
}
