using System.Globalization;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SporTakip.Application.Abstractions.Repositories;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Application.Services;
using SporTakip.Domain;
using Xunit;

namespace SporTakip.Application.Tests;

public class OlcumServiceTests
{
    private readonly FakeTakipRepository _takipler = new();
    private readonly FakeOlcumRepository _repo;
    private readonly OlcumService _sut;

    public OlcumServiceTests()
    {
        _repo = new FakeOlcumRepository(_takipler);
        _takipler.Items.Add(new Takip { Id = 3, SporcuId = 1, Tarih = DateTime.Today, Aciklama = "x" });
        _sut = new OlcumService(_repo, _takipler);
    }

    private static Olcum Gecerli() => new() { TakipId = 3, Tur = "Kilo", Deger = 72.5m, Birim = "kg" };

    [Fact]
    public void Dogrula_gecerli_olcum_basarili()
        => _sut.Dogrula(Gecerli()).IsValid.Should().BeTrue();

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Dogrula_bos_tur_hata_verir(string tur)
    {
        var o = Gecerli();
        o.Tur = tur;
        _sut.Dogrula(o).Errors.Should().ContainSingle().Which.PropertyName.Should().Be("Tur");
    }

    [Fact]
    public void Dogrula_tur_50_gecerli_51_hatali_ve_trim_edilir()
    {
        var o = Gecerli();
        o.Tur = " " + new string('a', 50) + " ";
        _sut.Dogrula(o).IsValid.Should().BeTrue();
        o.Tur = new string('a', 51);
        _sut.Dogrula(o).Errors.Should().ContainSingle().Which.PropertyName.Should().Be("Tur");
    }

    [Theory]
    [InlineData("9999999.99", true)]
    [InlineData("-9999999.99", true)]
    [InlineData("9999999.994", true)]
    [InlineData("9999999.995", false)]
    [InlineData("10000000", false)]
    [InlineData("-10000000", false)]
    public void Dogrula_deger_siniri(string deger, bool gecerli)
    {
        var o = Gecerli();
        o.Deger = decimal.Parse(deger, CultureInfo.InvariantCulture);
        var r = _sut.Dogrula(o);
        r.IsValid.Should().Be(gecerli);
        if (!gecerli) r.Errors.Should().ContainSingle().Which.PropertyName.Should().Be("Deger");
    }

    [Fact]
    public void Dogrula_birim_20_gecerli_21_hatali()
    {
        var o = Gecerli();
        o.Birim = new string('b', 20);
        _sut.Dogrula(o).IsValid.Should().BeTrue();
        o.Birim = new string('b', 21);
        _sut.Dogrula(o).Errors.Should().ContainSingle().Which.PropertyName.Should().Be("Birim");
    }

    [Fact]
    public void Dogrula_null_olcum_exception_firlatir()
        => FluentActions.Invoking(() => _sut.Dogrula(null!)).Should().Throw<ArgumentNullException>();

    [Fact]
    public async Task Ekle_normalize_eder_ve_id_dondurur()
    {
        var o = new Olcum { TakipId = 3, Tur = "  Kilo ", Deger = 72.456m, Birim = "   " };

        var r = await _sut.EkleAsync(o);

        r.IsValid.Should().BeTrue();
        r.Value.Should().Be(5);
        _repo.Eklenen!.Tur.Should().Be("Kilo");
        _repo.Eklenen.Deger.Should().Be(72.46m);
        _repo.Eklenen.Birim.Should().BeNull();
    }

    [Fact]
    public async Task Ekle_birimi_trim_eder()
    {
        var o = Gecerli();
        o.Birim = " kg ";
        await _sut.EkleAsync(o);
        _repo.Eklenen!.Birim.Should().Be("kg");
    }

    [Fact]
    public async Task Ekle_olmayan_takip_hata_verir_ve_kaydetmez()
    {
        var o = Gecerli();
        o.TakipId = 99;

        var r = await _sut.EkleAsync(o);

        r.IsValid.Should().BeFalse();
        r.Errors.Should().ContainSingle().Which.PropertyName.Should().Be("TakipId");
        _repo.Eklenen.Should().BeNull();
    }

    [Fact]
    public async Task Ekle_gecersiz_olcum_kaydetmez_ve_tum_hatalari_toplar()
    {
        var o = new Olcum { TakipId = 99, Tur = "", Deger = 1e9m, Birim = new string('x', 21) };

        var r = await _sut.EkleAsync(o);

        r.Errors.Select(e => e.PropertyName).Should().Equal("TakipId", "Tur", "Deger", "Birim");
        _repo.Eklenen.Should().BeNull();
    }

    [Fact]
    public async Task Guncelle_basarili_ve_yuvarlar()
    {
        var o = Gecerli();
        o.Id = 4;
        o.Deger = 1.005m;
        _repo.Items.Add(new Olcum { Id = 4, TakipId = 3, Tur = "Kilo", Deger = 1m });

        var r = await _sut.GuncelleAsync(o);

        r.IsValid.Should().BeTrue();
        _repo.Guncellenen!.Deger.Should().Be(1.01m);
    }

    [Fact]
    public async Task Guncelle_kayit_yoksa_Id_hatasi()
    {
        var o = Gecerli();
        o.Id = 4;

        var r = await _sut.GuncelleAsync(o);

        r.Errors.Should().ContainSingle().Which.PropertyName.Should().Be("Id");
    }

    [Fact]
    public async Task Guncelle_gecersizse_repoya_gitmez()
    {
        var o = Gecerli();
        o.Tur = "";

        var r = await _sut.GuncelleAsync(o);

        r.IsValid.Should().BeFalse();
        _repo.Guncellenen.Should().BeNull();
    }

    [Fact]
    public async Task Sil_repoya_devreder()
    {
        _repo.Items.Add(new Olcum { Id = 8, TakipId = 3, Tur = "Kilo" });
        (await _sut.SilAsync(8)).Should().BeTrue();
        _repo.Silinenler.Should().Equal(8);
    }


    [Fact]
    public async Task Sil_olmayan_kayit_false_doner()
        => (await _sut.SilAsync(8)).Should().BeFalse();

    [Fact]
    public async Task SporcuOlcumleri_yanlis_sporcu_bos_liste_doner()
    {
        KayitlariDoldur();
        (await _sut.SporcuOlcumleriAsync(2)).Should().BeEmpty();
        (await _repo.ListBySporcuIdAsync(2)).Should().BeEmpty();
    }

    [Fact]
    public async Task ListBySporcuId_tarih_ve_id_artan_siralar()
    {
        _takipler.Items.Add(new Takip { Id = 4, SporcuId = 1, Tarih = DateTime.Today.AddDays(-1), Aciklama = "e" });
        _repo.Items.Add(new Olcum { Id = 2, TakipId = 3, Tur = "A" });
        _repo.Items.Add(new Olcum { Id = 1, TakipId = 3, Tur = "B" });
        _repo.Items.Add(new Olcum { Id = 3, TakipId = 4, Tur = "C" });

        (await _repo.ListBySporcuIdAsync(1)).Select(k => k.Id).Should().Equal(3, 1, 2);
    }
    [Fact]
    public async Task TakipOlcumleri_yalniz_o_takibin_olcumlerini_dondurur()
    {
        _repo.Items.Add(new Olcum { Id = 1, TakipId = 3, Tur = "Kilo" });
        _repo.Items.Add(new Olcum { Id = 2, TakipId = 4, Tur = "Boy" });

        (await _sut.TakipOlcumleriAsync(3)).Select(o => o.Id).Should().Equal(1);
    }

    private void KayitlariDoldur()
    {
        _repo.Items.Add(new Olcum { Id = 1, TakipId = 3, Tur = "Kilo", Deger = 70, Birim = "kg" });
        _repo.Items.Add(new Olcum { Id = 2, TakipId = 3, Tur = "Işık", Deger = 1, Birim = null });
        _repo.Items.Add(new Olcum { Id = 3, TakipId = 3, Tur = "kilo", Deger = 71, Birim = "kg" });
        _repo.Items.Add(new Olcum { Id = 4, TakipId = 3, Tur = "İp", Deger = 2, Birim = null });
        _repo.Items.Add(new Olcum { Id = 5, TakipId = 3, Tur = "Boy", Deger = 180, Birim = "cm" });
    }

    [Fact]
    public async Task SporcuOlcumleri_tursuz_hepsini_dondurur()
    {
        KayitlariDoldur();
        (await _sut.SporcuOlcumleriAsync(1)).Should().HaveCount(5);
        (await _sut.SporcuOlcumleriAsync(1, "  ")).Should().HaveCount(5);
    }

    [Fact]
    public async Task SporcuOlcumleri_turle_filtreler_buyuk_kucuk_duyarsiz()
    {
        KayitlariDoldur();
        (await _sut.SporcuOlcumleriAsync(1, " KİLO ")).Select(o => o.Id).Should().Equal(1, 3);
        (await _sut.SporcuOlcumleriAsync(1, "Yok")).Should().BeEmpty();
    }

    [Fact]
    public async Task SporcuOlcumleri_tr_TR_buyuk_kucuk_i_ayrimi()
    {
        KayitlariDoldur();
        (await _sut.SporcuOlcumleriAsync(1, "ışık")).Select(o => o.Id).Should().Equal(2);
        (await _sut.SporcuOlcumleriAsync(1, "ip")).Select(o => o.Id).Should().Equal(4);
        (await _sut.SporcuOlcumleriAsync(1, "Ip")).Should().BeEmpty();
    }

    [Fact]
    public async Task Turleri_distinct_ve_tr_siralidir()
    {
        KayitlariDoldur();
        (await _sut.TurleriAsync(1)).Should().Equal("Boy", "Işık", "İp", "Kilo");
    }

    [Fact]
    public async Task Turleri_bos_listede_bos_doner()
        => (await _sut.TurleriAsync(1)).Should().BeEmpty();

    [Fact]
    public void DI_OlcumService_kaydedilir()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IOlcumRepository>(_repo);
        services.AddSingleton<ITakipRepository>(_takipler);
        services.AddSporTakipApplication();
        using var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();
        scope.ServiceProvider.GetRequiredService<IOlcumService>().Should().BeOfType<OlcumService>();
    }
}
