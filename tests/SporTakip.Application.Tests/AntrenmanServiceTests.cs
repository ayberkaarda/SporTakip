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
}
