using FluentAssertions;
using SporTakip.Application.Services;
using SporTakip.Application.Validation;
using SporTakip.Domain;
using Xunit;

namespace SporTakip.Application.Tests;

public class SporcuServiceTests
{
    private readonly FakeSporcuRepository _repo = new();
    private readonly SporcuService _sut;

    public SporcuServiceTests() => _sut = new SporcuService(_repo);

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Dogrula_bos_ad_hata_verir(string? ad)
    {
        var sonuc = _sut.Dogrula(new Sporcu { Ad = ad!, SporDali = "Yüzme" });

        sonuc.IsValid.Should().BeFalse();
        sonuc.Errors.Should().ContainSingle()
            .Which.Should().Be(new ValidationError("Ad", "Sporcu adı boş olamaz."));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Dogrula_bos_spor_dali_hata_verir(string? dal)
    {
        var sonuc = _sut.Dogrula(new Sporcu { Ad = "Ali", SporDali = dal });

        sonuc.Errors.Should().ContainSingle()
            .Which.PropertyName.Should().Be("SporDali");
    }

    [Fact]
    public void Dogrula_ad_cok_uzunsa_hata_verir()
    {
        var sonuc = _sut.Dogrula(new Sporcu { Ad = new string('a', ValidationConstants.SporcuAdMaxLength + 1), SporDali = "x" });

        sonuc.Errors.Should().ContainSingle().Which.PropertyName.Should().Be("Ad");
    }

    [Fact]
    public void Dogrula_spor_dali_cok_uzunsa_hata_verir()
    {
        var sonuc = _sut.Dogrula(new Sporcu { Ad = "Ali", SporDali = new string('a', ValidationConstants.SporDaliMaxLength + 1) });

        sonuc.Errors.Should().ContainSingle().Which.PropertyName.Should().Be("SporDali");
    }

    [Fact]
    public void Dogrula_tam_uzunluktaki_alanlar_gecerlidir()
    {
        var sonuc = _sut.Dogrula(new Sporcu
        {
            Ad = new string('a', ValidationConstants.SporcuAdMaxLength),
            SporDali = new string('b', ValidationConstants.SporDaliMaxLength)
        });

        sonuc.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Dogrula_trim_sonrasi_uzunluga_bakar()
    {
        var sonuc = _sut.Dogrula(new Sporcu { Ad = "  " + new string('a', ValidationConstants.SporcuAdMaxLength) + "  ", SporDali = "x" });

        sonuc.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Dogrula_iki_hata_birlikte_doner()
    {
        var sonuc = _sut.Dogrula(new Sporcu { Ad = "", SporDali = "" });

        sonuc.Errors.Select(e => e.PropertyName).Should().BeEquivalentTo("Ad", "SporDali");
    }

    [Fact]
    public async Task EkleAsync_gecerliyse_trimleyip_ekler_ve_id_doner()
    {
        var sonuc = await _sut.EkleAsync(new Sporcu { Ad = "  Ali  ", SporDali = " Yüzme " });

        sonuc.IsValid.Should().BeTrue();
        sonuc.Value.Should().Be(42);
        _repo.Eklenen!.Ad.Should().Be("Ali");
        _repo.Eklenen.SporDali.Should().Be("Yüzme");
    }

    [Fact]
    public async Task EkleAsync_gecersizse_repoya_gitmez()
    {
        var sonuc = await _sut.EkleAsync(new Sporcu { Ad = " ", SporDali = "x" });

        sonuc.IsValid.Should().BeFalse();
        _repo.Eklenen.Should().BeNull();
    }

    [Fact]
    public async Task GuncelleAsync_gecerliyse_gunceller()
    {
        var sonuc = await _sut.GuncelleAsync(new Sporcu { Id = 3, Ad = " Veli ", SporDali = "Koşu" });

        sonuc.IsValid.Should().BeTrue();
        _repo.Guncellenen!.Ad.Should().Be("Veli");
    }

    [Fact]
    public async Task GuncelleAsync_gecersizse_repoya_gitmez()
    {
        var sonuc = await _sut.GuncelleAsync(new Sporcu { Id = 3, Ad = "", SporDali = "Koşu" });

        sonuc.IsValid.Should().BeFalse();
        _repo.Guncellenen.Should().BeNull();
    }

    [Fact]
    public async Task GuncelleAsync_kayit_yoksa_hata_doner()
    {
        _repo.UpdateSonucu = false;

        var sonuc = await _sut.GuncelleAsync(new Sporcu { Id = 99, Ad = "Ali", SporDali = "Koşu" });

        sonuc.IsValid.Should().BeFalse();
        sonuc.Errors.Should().ContainSingle().Which.Message.Should().Be("Sporcu bulunamadı.");
    }

    [Fact]
    public async Task SilAsync_SporcuyuTumuyleSilAsync_cagirir()
    {
        var sonuc = await _sut.SilAsync(5);

        sonuc.Should().BeTrue();
        _repo.TumuyleSilinenler.Should().Equal(5);
        _repo.DuzSilinenler.Should().BeEmpty();
    }

    [Fact]
    public async Task SilAsync_repo_false_donerse_false_doner()
    {
        _repo.TumuyleSilSonucu = false;

        (await _sut.SilAsync(5)).Should().BeFalse();
    }

    [Fact]
    public async Task ListeleAsync_ve_GetirAsync_repodan_okur()
    {
        _repo.Items.Add(new Sporcu { Id = 1, Ad = "A", SporDali = "B" });

        (await _sut.ListeleAsync()).Should().HaveCount(1);
        (await _sut.GetirAsync(1))!.Ad.Should().Be("A");
        (await _sut.GetirAsync(2)).Should().BeNull();
    }

    private void Tohumla()
    {
        _repo.Items.AddRange(
        [
            new Sporcu { Id = 1, Ad = "Zeynep Kaya", SporDali = "Yüzme" },
            new Sporcu { Id = 2, Ad = "Ali Işık", SporDali = "Basketbol" },
            new Sporcu { Id = 3, Ad = "İlker Demir", SporDali = null },
            new Sporcu { Id = 4, Ad = "Can Yıldız", SporDali = "Atletizm" },
        ]);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task AraAsync_bos_arama_tumunu_ad_sirali_doner(string? arama)
    {
        Tohumla();
        var sonuc = await _sut.AraAsync(arama);
        sonuc.Select(s => s.Id).Should().Equal(2, 4, 3, 1);
    }

    [Fact]
    public async Task AraAsync_kismi_eslesme_adda_bulur()
    {
        Tohumla();
        (await _sut.AraAsync("  eyn")).Select(s => s.Id).Should().Equal(1);
        (await _sut.AraAsync("KAY")).Select(s => s.Id).Should().Equal(1);
    }

    [Theory]
    [InlineData("IŞIK", 2)]
    [InlineData("ışık", 2)]
    [InlineData("ilker", 3)]
    [InlineData("İLKER", 3)]
    [InlineData("yıldız", 4)]
    [InlineData("YILDIZ", 4)]
    public async Task AraAsync_turkce_i_ve_dotsuz_i_dogru_eslesir(string arama, int beklenenId)
    {
        Tohumla();
        var sonuc = await _sut.AraAsync(arama);
        sonuc.Select(s => s.Id).Should().Equal(beklenenId);
    }

    [Fact]
    public async Task AraAsync_noktali_i_ile_noktasiz_i_karismaz()
    {
        Tohumla();
        (await _sut.AraAsync("is")).Should().BeEmpty();
        (await _sut.AraAsync("işık")).Should().BeEmpty();
        (await _sut.AraAsync("ilker")).Select(s => s.Id).Should().Equal(3);
        (await _sut.AraAsync("ış")).Select(s => s.Id).Should().Equal(2);
    }

    [Fact]
    public async Task AraAsync_spor_dali_ile_arar_ve_null_dalli_sporcuda_hata_vermez()
    {
        Tohumla();
        (await _sut.AraAsync("basket")).Select(s => s.Id).Should().Equal(2);
        (await _sut.AraAsync("YÜZ")).Select(s => s.Id).Should().Equal(1);
    }

    [Fact]
    public async Task AraAsync_ad_veya_dalda_eslesenler_ad_sirali_doner()
    {
        Tohumla();
        var sonuc = await _sut.AraAsync("a");
        sonuc.Select(s => s.Id).Should().Equal(2, 4, 1);
    }

    [Fact]
    public async Task AraAsync_eslesme_yoksa_bos_doner()
    {
        Tohumla();
        (await _sut.AraAsync("xyz")).Should().BeEmpty();
    }

    [Fact]
    public async Task AraAsync_turkce_alfabeye_gore_siralar()
    {
        _repo.Items.AddRange(
        [
            new Sporcu { Id = 1, Ad = "Zeki", SporDali = "x" },
            new Sporcu { Id = 2, Ad = "çağla", SporDali = "x" },
            new Sporcu { Id = 3, Ad = "Can", SporDali = "x" },
            new Sporcu { Id = 4, Ad = "Ahmet", SporDali = "x" },
        ]);
        (await _sut.AraAsync("x")).Select(s => s.Id).Should().Equal(4, 3, 2, 1);
    }
}
