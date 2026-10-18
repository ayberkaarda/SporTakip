
# 🏋️‍♂️ Spor Takip Uygulaması

Bu uygulama, sporcuların antrenman ve gelişim süreçlerini takip etmek amacıyla geliştirilmiş bir masaüstü yazılımıdır. C# Windows Forms ile hazırlanmış olup SQL Server veritabanı kullanmaktadır.

## 🚀 Kurulum

**Gereksinimler**

- Windows
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server LocalDB (Visual Studio ile gelir) veya SQL Server Express

1. **Veritabanını Oluşturma**  
   Komutları proje kök klasöründe çalıştırın. Önce boş veritabanını açın, sonra şema betiklerini sırayla uygulayın:

   ```bat
   sqlcmd -S "(localdb)\MSSQLLocalDB" -Q "CREATE DATABASE SporTakip"
   sqlcmd -S "(localdb)\MSSQLLocalDB" -d SporTakip -i database\001_initial.sql
   sqlcmd -S "(localdb)\MSSQLLocalDB" -d SporTakip -i database\002_cascade_fk.sql
   ```

   Örnek veri isterseniz (isteğe bağlı):

   ```bat
   sqlcmd -S "(localdb)\MSSQLLocalDB" -d SporTakip -i database\seed.sql
   ```

   Betikler tekrar çalıştırılabilir; var olan tablo ve kayıtlara dokunmaz.

2. **Bağlantı Ayarı**  
   Varsayılan bağlantı `src\SporTakip.WinForms\appsettings.json` içindedir ve LocalDB'yi kullanır. Kendi SQL Server'ınızı kullanacaksanız aynı klasöre `appsettings.Local.json` dosyası açın (git'e eklenmez, varsa varsayılanı ezer):

   ```json
   {
     "ConnectionStrings": {
       "SporTakip": "Server=.\\SQLEXPRESS;Database=SporTakip;Integrated Security=True;TrustServerCertificate=True;"
     }
   }
   ```

3. **Programı Çalıştırma**  
   Visual Studio ile `SporTakip.sln` dosyasını açıp başlatabilir ya da komut satırından çalıştırabilirsiniz:

   ```bat
   dotnet run --project src\SporTakip.WinForms
   ```

**Alternatif: Yedekten Yükleme**  
Eski yöntemle, proje klasöründeki `SporTakip.bak` dosyasını kendi SQL Server sunucunuza **restore** edebilirsiniz. Bu durumda yalnızca `database\002_cascade_fk.sql` betiğini çalıştırmanız yeterlidir (sporcu silindiğinde kayıtlarının da silinmesi için).

---

## 🛠️ Geliştirme

**Çözüm yapısı**

```
src/
  SporTakip.Domain/        Varlıklar (Sporcu, Antrenman, Takip)
  SporTakip.Application/   Servisler ve doğrulama kuralları
  SporTakip.Data/          Dapper ile SQL Server repository'leri
  SporTakip.WinForms/      Arayüz, DI ve appsettings.json
tests/
  SporTakip.Application.Tests/   Servis birim testleri
  SporTakip.Data.Tests/          Repository entegrasyon testleri
database/                  Şema ve örnek veri betikleri
```

**Katmanlar ve bağımlılık yönü**

```
WinForms ──> Application ──> Domain
   │              ▲
   └──> Data ─────┘ (Application arayüzlerini uygular)
```

**Derleme ve test**

```bat
dotnet build
dotnet test
```

Entegrasyon testleri (`SporTakip.Data.Tests`) yalnızca `SPORTAKIP_TEST_CONNECTION` ortam değişkeni tanımlıysa çalışır, yoksa atlanır. Testler bu sunucuda geçici bir veritabanı açar, şema betiklerini uygular ve sonunda siler:

```bat
set SPORTAKIP_TEST_CONNECTION=Server=(localdb)\MSSQLLocalDB;Integrated Security=True;TrustServerCertificate=True;
dotnet test
```

---

## 👤 Sporcu Ekleme

1. `Sporcu Adı` kısmına adı yazın (örn: **Ayberk Arda**)  
2. `Spor Dalı` kısmına branşı yazın (örn: **Yüzme**)  
3. `Kaydet` butonuna basın  
4. Sporcu listede görünür

**Arama, düzenleme ve silme**

- Listenin üstündeki arama kutusuna yazdıkça sporcular ada veya branşa göre süzülür. `Enter` aramayı hemen yapar, `Esc` kutuyu temizler.
- Sporcuyu düzenlemek için listeden seçip `Düzenle` butonuna basın ya da sporcuya çift tıklayın.
- `Sil` butonu sporcuyu, onay alındıktan sonra ona ait tüm antrenman ve takip kayıtlarıyla birlikte siler.

---

## 🏃‍♂️ Antrenman Ekleme

1. Ana ekranda `Antrenman Ekle` butonuna tıklayın  
2. Açılan pencerede:  
   - Sporcu listesinden birini seçin  
   - Antrenman adını girin (örn: **Göğüs Günü**)  
   - Detaylarını yazın (örn: **Chest Press, Incline Dumbbell Press**)  
3. `Kaydet` butonuna tıklayın

---

## 📅 Takip Kaydı Ekleme

1. Ana ekranda `Takip Ekle` butonuna tıklayın  
2. Açılan formda:  
   - Sporcu seçin  
   - Takip tarihini belirleyin  
   - Açıklama girin (örn: **Ağırlık: 95kg, setler tamamlandı**)  
3. `Kaydet` butonuna tıklayın

---

## 📊 Sporcu Geçmişi Görüntüleme

1. Ana ekranda `Sporcu Detayı` butonuna tıklayın  
2. Açılan formda:  
   - Sporcu seçin  
   - Seçilen sporcuya ait antrenmanlar ve takip kayıtları listelenir  
   - İsteğe bağlı olarak `Listeyi Güncelle` butonuna basarak verileri yenileyebilirsiniz

**Antrenman ve takip kayıtlarını düzenleme / silme**

- Bir kaydı düzenlemek için tablodan seçip altındaki `Düzenle` butonuna basın ya da satıra çift tıklayın. Antrenmanda ad ve detaylar, takipte tarih ve açıklama değiştirilebilir.
- Silmek için kaydı seçip `Sil` butonuna basın ya da `Delete` tuşunu kullanın; silmeden önce onay istenir.

---

## 🖼️ Ekran Görüntüleri

### Ana Sayfa
![Ana Sayfa](screenshots/anasayfa.png)

### Antrenman Ekle
![Antreman Ekle](screenshots/antreman_ekle.png)

### Takip Kaydı Ekle
![Takip Ekle](screenshots/takip_ekle.png)

### Sporcu Detayı
![Sporcu Detayi](screenshots/sporcu_detayi.png)
