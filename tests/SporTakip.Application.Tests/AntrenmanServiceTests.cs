using FluentAssertions;
using SporTakip.Application.Services;
using SporTakip.Application.Validation;
using SporTakip.Domain;
using Xunit;

namespace SporTakip.Application.Tests;

public class AntrenmanServiceTests
{
    private readonly FakeSporcuRepository _sporcular = new();
    private readonly FakeAntrenmanRepository _repo = new();
    private readonly AntrenmanService _sut;

    public AntrenmanServiceTests()
    {
        _sporcular.Items.Add(new Sporcu { Id = 1, Ad = "Ali", SporDali = "Yüzme" });
        _sut = new AntrenmanService(_repo, _sporcular);
    }

    private static Antrenman Gecerli() => new() { SporcuId = 1, AntrenmanAdi = "Sabah", Detaylar = "5 km" };

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Dogrula_bos_ad_hata_verir(string? ad)
    {
        var a = Gecerli();
        a.AntrenmanAdi = ad;

        _sut.Dogrula(a).Errors.Should().ContainSingle()
            .Which.Should().Be(new ValidationError("AntrenmanAdi", "Antrenman adı boş olamaz."));
    }

    [Fact]
    public void Dogrula_ad_100u_asarsa_hata_verir()
    {
        var a = Gecerli();
        a.AntrenmanAdi = new string('x', ValidationConstants.AntrenmanAdiMaxLength + 1);

        _sut.Dogrula(a).Errors.Should().ContainSingle().Which.PropertyName.Should().Be("AntrenmanAdi");
    }

    [Fact]
    public void Dogrula_ad_tam_100_gecerlidir()
    {
        var a = Gecerli();
        a.AntrenmanAdi = new string('x', ValidationConstants.AntrenmanAdiMaxLength);

        _sut.Dogrula(a).IsValid.Should().BeTrue();
    }

    [Fact]
    public void Dogrula_detay_opsiyoneldir()
    {
        var a = Gecerli();
        a.Detaylar = null;

        _sut.Dogrula(a).IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task EkleAsync_sporcu_yoksa_SporcuId_hatasi_verir()
    {
        var a = Gecerli();
        a.SporcuId = 999;

        var sonuc = await _sut.EkleAsync(a);

        sonuc.IsValid.Should().BeFalse();
        sonuc.Errors.Should().ContainSingle().Which.PropertyName.Should().Be("SporcuId");
        _repo.Eklenen.Should().BeNull();
    }

    [Fact]
    public async Task EkleAsync_sporcu_yok_ve_ad_bos_iki_hata_verir()
    {
        var sonuc = await _sut.EkleAsync(new Antrenman { SporcuId = 999, AntrenmanAdi = "" });

        sonuc.Errors.Select(e => e.PropertyName).Should().BeEquivalentTo("SporcuId", "AntrenmanAdi");
    }

    [Fact]
    public async Task EkleAsync_gecerliyse_trimleyip_ekler()
    {
        var a = new Antrenman { SporcuId = 1, AntrenmanAdi = "  Sabah  ", Detaylar = "  5 km  " };

        var sonuc = await _sut.EkleAsync(a);

        sonuc.IsValid.Should().BeTrue();
        sonuc.Value.Should().Be(7);
        _repo.Eklenen!.AntrenmanAdi.Should().Be("Sabah");
        _repo.Eklenen.Detaylar.Should().Be("5 km");
    }

    [Fact]
    public async Task EkleAsync_bos_detay_null_olur()
    {
        var a = Gecerli();
        a.Detaylar = "   ";

        await _sut.EkleAsync(a);

        _repo.Eklenen!.Detaylar.Should().BeNull();
    }

    [Fact]
    public async Task GuncelleAsync_gecerliyse_gunceller()
    {
        var sonuc = await _sut.GuncelleAsync(new Antrenman { Id = 4, SporcuId = 1, AntrenmanAdi = " Akşam " });

        sonuc.IsValid.Should().BeTrue();
        _repo.Guncellenen!.AntrenmanAdi.Should().Be("Akşam");
    }

    [Fact]
    public async Task GuncelleAsync_sporcu_yoksa_hata_verir()
    {
        var a = Gecerli();
        a.SporcuId = 999;

        var sonuc = await _sut.GuncelleAsync(a);

        sonuc.Errors.Should().ContainSingle().Which.PropertyName.Should().Be("SporcuId");
        _repo.Guncellenen.Should().BeNull();
    }

    [Fact]
    public async Task GuncelleAsync_kayit_yoksa_hata_verir()
    {
        _repo.UpdateSonucu = false;

        var sonuc = await _sut.GuncelleAsync(Gecerli());

        sonuc.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task SilAsync_repoya_yonlendirir()
    {
        (await _sut.SilAsync(8)).Should().BeTrue();
        _repo.Silinenler.Should().Equal(8);
    }

    [Fact]
    public async Task SporcuyaGoreListeleAsync_sadece_ilgili_sporcunun_kayitlarini_doner()
    {
        _repo.Items.Add(new Antrenman { Id = 1, SporcuId = 1, AntrenmanAdi = "A" });
        _repo.Items.Add(new Antrenman { Id = 2, SporcuId = 2, AntrenmanAdi = "B" });

        var liste = await _sut.SporcuyaGoreListeleAsync(1);

        liste.Should().ContainSingle().Which.Id.Should().Be(1);
    }

    [Theory]
    [InlineData(null, true)]
    [InlineData(0, false)]
    [InlineData(1, true)]
    [InlineData(7, true)]
    [InlineData(8, false)]
    [InlineData(-1, false)]
    public void Dogrula_gun_sinirlari(int? gun, bool gecerli)
    {
        var a = Gecerli();
        a.Gun = gun;

        var sonuc = _sut.Dogrula(a);

        sonuc.IsValid.Should().Be(gecerli);
        if (!gecerli) sonuc.Errors.Should().ContainSingle().Which.PropertyName.Should().Be("Gun");
    }

    [Fact]
    public async Task EkleAsync_gunu_oldugu_gibi_gecirir()
    {
        var a = Gecerli();
        a.Gun = 3;
        await _sut.EkleAsync(a);
        _repo.Eklenen!.Gun.Should().Be(3);
    }

    [Fact]
    public async Task GuncelleAsync_gunu_oldugu_gibi_gecirir()
    {
        var a = Gecerli();
        a.Gun = 5;
        await _sut.GuncelleAsync(a);
        _repo.Guncellenen!.Gun.Should().Be(5);
    }

    [Fact]
    public async Task EkleAsync_gecersiz_gun_eklemez()
    {
        var a = Gecerli();
        a.Gun = 8;
        var sonuc = await _sut.EkleAsync(a);
        sonuc.IsValid.Should().BeFalse();
        _repo.Eklenen.Should().BeNull();
    }

    [Fact]
    public async Task HaftalikProgramAsync_bos_sporcu_icin_7_bos_gun_doner()
    {
        var p = await _sut.HaftalikProgramAsync(1);

        p.Select(g => g.GunAdi).Should().Equal("Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi", "Pazar");
        p.Select(g => g.Gun).Should().Equal(1, 2, 3, 4, 5, 6, 7);
        p.Should().OnlyContain(g => g.Antrenmanlar.Count == 0);
    }

    [Fact]
    public async Task HaftalikProgramAsync_gunlere_gruplar_ve_ada_gore_siralar()
    {
        _repo.Items.Add(new Antrenman { Id = 1, SporcuId = 1, AntrenmanAdi = "Zeta", Gun = 1 });
        _repo.Items.Add(new Antrenman { Id = 2, SporcuId = 1, AntrenmanAdi = "alfa", Gun = 1 });
        _repo.Items.Add(new Antrenman { Id = 3, SporcuId = 1, AntrenmanAdi = "Orta", Gun = 7 });
        _repo.Items.Add(new Antrenman { Id = 4, SporcuId = 2, AntrenmanAdi = "Başkası", Gun = 1 });

        var p = await _sut.HaftalikProgramAsync(1);

        p.Should().HaveCount(7);
        p[0].Antrenmanlar.Select(a => a.AntrenmanAdi).Should().Equal("alfa", "Zeta");
        p[6].Antrenmanlar.Should().ContainSingle().Which.Id.Should().Be(3);
        p[1].Antrenmanlar.Should().BeEmpty();
    }

    [Fact]
    public async Task HaftalikProgramAsync_turkce_I_ve_i_harflerini_tr_TR_sirasina_gore_siralar()
    {
        var adlar = new[] { "Zeta", "iğne", "İstif", "Işık", "Çevik", "ılık", "alfa" };
        var id = 1;
        foreach (var ad in adlar)
            _repo.Items.Add(new Antrenman { Id = id++, SporcuId = 1, AntrenmanAdi = ad, Gun = 3 });

        var p = await _sut.HaftalikProgramAsync(1);

        p[2].Antrenmanlar.Select(a => a.AntrenmanAdi)
            .Should().Equal("alfa", "Çevik", "ılık", "Işık", "iğne", "İstif", "Zeta");
    }

    [Fact]
    public async Task HaftalikProgramAsync_ayni_adli_antrenmanlari_Id_sirasina_gore_siralar()
    {
        _repo.Items.Add(new Antrenman { Id = 9, SporcuId = 1, AntrenmanAdi = "Aynı", Gun = 2 });
        _repo.Items.Add(new Antrenman { Id = 3, SporcuId = 1, AntrenmanAdi = "Aynı", Gun = 2 });
        _repo.Items.Add(new Antrenman { Id = 5, SporcuId = 1, AntrenmanAdi = "Aynı", Gun = 2 });

        var p = await _sut.HaftalikProgramAsync(1);

        p[1].Antrenmanlar.Select(a => a.Id).Should().Equal(3, 5, 9);
    }

    [Fact]
    public async Task HaftalikProgramAsync_atanmamislar_sonda_Gun_0_ile_eklenir()
    {
        _repo.Items.Add(new Antrenman { Id = 1, SporcuId = 1, AntrenmanAdi = "B", Gun = null });
        _repo.Items.Add(new Antrenman { Id = 2, SporcuId = 1, AntrenmanAdi = "A", Gun = null });
        _repo.Items.Add(new Antrenman { Id = 3, SporcuId = 1, AntrenmanAdi = "C", Gun = 2 });

        var p = await _sut.HaftalikProgramAsync(1);

        p.Should().HaveCount(8);
        p[7].Gun.Should().Be(0);
        p[7].GunAdi.Should().Be("Atanmamış");
        p[7].Antrenmanlar.Select(a => a.AntrenmanAdi).Should().Equal("A", "B");
        p[1].Antrenmanlar.Should().ContainSingle();
    }

    [Fact]
    public async Task HaftalikProgramAsync_atanmamis_yoksa_8_inci_giris_yoktur()
    {
        _repo.Items.Add(new Antrenman { Id = 1, SporcuId = 1, AntrenmanAdi = "A", Gun = 4 });

        (await _sut.HaftalikProgramAsync(1)).Should().HaveCount(7);
    }
}
