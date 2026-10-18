using FluentAssertions;
using SporTakip.Application.Services;
using SporTakip.Application.Validation;
using SporTakip.Domain;
using Xunit;

namespace SporTakip.Application.Tests;

public class TakipServiceTests
{
    private readonly FakeSporcuRepository _sporcular = new();
    private readonly FakeTakipRepository _repo = new();
    private readonly TakipService _sut;

    public TakipServiceTests()
    {
        _sporcular.Items.Add(new Sporcu { Id = 1, Ad = "Ali", SporDali = "Yüzme" });
        _sut = new TakipService(_repo, _sporcular);
    }

    private static Takip Gecerli() => new() { SporcuId = 1, Tarih = DateTime.Today, Aciklama = "İyi gitti" };

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Dogrula_bos_aciklama_hata_verir(string? aciklama)
    {
        var t = Gecerli();
        t.Aciklama = aciklama;

        _sut.Dogrula(t).Errors.Should().ContainSingle()
            .Which.Should().Be(new ValidationError("Aciklama", "Açıklama boş olamaz."));
    }

    [Fact]
    public void Dogrula_gelecek_tarih_hata_verir()
    {
        var t = Gecerli();
        t.Tarih = DateTime.Today.AddDays(1);

        _sut.Dogrula(t).Errors.Should().ContainSingle()
            .Which.Should().Be(new ValidationError("Tarih", "Tarih bugünden ileri olamaz."));
    }

    [Fact]
    public void Dogrula_varsayilan_ve_1753_oncesi_tarih_hata_verir()
    {
        foreach (var tarih in new[] { default(DateTime), new DateTime(1752, 12, 31) })
        {
            var t = Gecerli();
            t.Tarih = tarih;

            _sut.Dogrula(t).Errors.Should().ContainSingle()
                .Which.PropertyName.Should().Be("Tarih");
        }
    }

    [Fact]
    public void Dogrula_1753_01_01_gecerlidir()
    {
        var t = Gecerli();
        t.Tarih = new DateTime(1753, 1, 1);

        _sut.Dogrula(t).IsValid.Should().BeTrue();
    }

    [Fact]
    public void Dogrula_bugun_ve_gecmis_tarih_gecerlidir()
    {
        var bugun = Gecerli();
        var gecmis = Gecerli();
        gecmis.Tarih = DateTime.Today.AddYears(-1);

        _sut.Dogrula(bugun).IsValid.Should().BeTrue();
        _sut.Dogrula(gecmis).IsValid.Should().BeTrue();
    }

    [Fact]
    public void Dogrula_bugunun_ilerleyen_saati_gecerlidir()
    {
        var t = Gecerli();
        t.Tarih = DateTime.Today.AddHours(23).AddMinutes(59);

        _sut.Dogrula(t).IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task EkleAsync_sporcu_yoksa_SporcuId_hatasi_verir()
    {
        var t = Gecerli();
        t.SporcuId = 999;

        var sonuc = await _sut.EkleAsync(t);

        sonuc.Errors.Should().ContainSingle().Which.PropertyName.Should().Be("SporcuId");
        _repo.Eklenen.Should().BeNull();
    }

    [Fact]
    public async Task EkleAsync_gecersizse_tum_hatalari_doner()
    {
        var sonuc = await _sut.EkleAsync(new Takip
        {
            SporcuId = 999,
            Tarih = DateTime.Today.AddDays(2),
            Aciklama = ""
        });

        sonuc.Errors.Select(e => e.PropertyName).Should().BeEquivalentTo("SporcuId", "Tarih", "Aciklama");
    }

    [Fact]
    public async Task EkleAsync_gecerliyse_aciklamayi_trimleyip_ekler()
    {
        var t = Gecerli();
        t.Aciklama = "  İyi gitti  ";

        var sonuc = await _sut.EkleAsync(t);

        sonuc.IsValid.Should().BeTrue();
        sonuc.Value.Should().Be(9);
        _repo.Eklenen!.Aciklama.Should().Be("İyi gitti");
    }

    [Fact]
    public async Task GuncelleAsync_gecerliyse_gunceller()
    {
        var t = Gecerli();
        t.Id = 5;

        var sonuc = await _sut.GuncelleAsync(t);

        sonuc.IsValid.Should().BeTrue();
        _repo.Guncellenen!.Id.Should().Be(5);
    }

    [Fact]
    public async Task GuncelleAsync_gecersizse_repoya_gitmez()
    {
        var t = Gecerli();
        t.Aciklama = " ";

        (await _sut.GuncelleAsync(t)).IsValid.Should().BeFalse();
        _repo.Guncellenen.Should().BeNull();
    }

    [Fact]
    public async Task GuncelleAsync_kayit_yoksa_hata_verir()
    {
        _repo.UpdateSonucu = false;

        (await _sut.GuncelleAsync(Gecerli())).IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task SilAsync_repoya_yonlendirir()
    {
        (await _sut.SilAsync(3)).Should().BeTrue();
        _repo.Silinenler.Should().Equal(3);
    }

    [Fact]
    public async Task SporcuyaGoreListeleAsync_tarihe_gore_azalan_doner()
    {
        _repo.Items.Add(new Takip { Id = 1, SporcuId = 1, Tarih = new DateTime(2024, 1, 1), Aciklama = "a" });
        _repo.Items.Add(new Takip { Id = 2, SporcuId = 1, Tarih = new DateTime(2024, 3, 1), Aciklama = "b" });
        _repo.Items.Add(new Takip { Id = 3, SporcuId = 2, Tarih = new DateTime(2024, 2, 1), Aciklama = "c" });

        var liste = await _sut.SporcuyaGoreListeleAsync(1);

        liste.Select(t => t.Id).Should().Equal(2, 1);
    }
}
