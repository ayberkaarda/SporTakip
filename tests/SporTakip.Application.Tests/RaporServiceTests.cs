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
    private readonly RaporService _sut;

    public RaporServiceTests() => _sut = new RaporService(_sporcular, _antrenmanlar, _takipler);

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
        services.AddSporTakipApplication();

        using var provider = services.BuildServiceProvider(validateScopes: true);
        using var scope = provider.CreateScope();

        scope.ServiceProvider.GetRequiredService<ISporcuService>().Should().BeOfType<SporcuService>();
        scope.ServiceProvider.GetRequiredService<IAntrenmanService>().Should().BeOfType<AntrenmanService>();
        scope.ServiceProvider.GetRequiredService<ITakipService>().Should().BeOfType<TakipService>();
        scope.ServiceProvider.GetRequiredService<IRaporService>().Should().BeOfType<RaporService>();
    }
}
