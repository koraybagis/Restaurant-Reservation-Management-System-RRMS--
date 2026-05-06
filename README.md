Restoran Rezervasyon Sistemi Bu proje, bir restoranın operasyonel süreçlerini dijitalleştirmek, personel koordinasyonunu optimize etmek ve rezervasyon yönetimini hatasız bir şekilde yürütmek amacıyla C# Windows Forms mimarisi ve Microsoft SQL Server veritabanı kullanılarak geliştirilmiş uçtan uca bir otomasyon çözümüdür.

🚀 Proje Hakkında Sistem, sadece basit bir veri kayıt aracı olmanın ötesinde; yönetici, garson ve temizlik personeli rollerini kapsayan, yaşayan bir işletme ekosistemi sunar. Dinamik masa planı takibi, SMTP destekli e-posta doğrulama ve yetki tabanlı işlem hiyerarşisi ile profesyonel bir işletme yönetimi sağlar.

🛠️ Teknik Özellikler Dinamik Masa Yönetimi: Masaların anlık durumlarını (Boş, Dolu, Kirli) saniye bazlı takip eden ve rezervasyon saati yaklaştığında görsel uyarı veren akıllı takip sistemi.

Personel Rol Hiyerarşisi: Yönetici, Garson ve Temizlik Personeli için ayrıştırılmış özel paneller ve yetkilendirme katmanları.

Operasyonel İş Akışı Döngüsü: Garson tarafından hesabı kapatılan masanın otomatik olarak "Kirli" statüsüne geçmesi ve temizlik ekibi onayı gelmeden yeni rezervasyon kabul etmemesi.

Menü ve Ürün Yönetimi: Admin paneli üzerinden resim destekli ürün ekleme, silme ve fiyat güncelleme fonksiyonları.

Güvenlik Protokolleri: SMTP üzerinden iletilen 6 haneli OTP (Tek Kullanımlık Şifre) ile rezervasyon doğrulama ve kullanıcı kısıtlama (banlama) mekanizması.

Gelişmiş Veri Eşleme: Masaya tıklandığında ilgili rezervasyon ve hesap detaylarının hiçbir manuel giriş gerektirmeden otomatik olarak ekrana getirilmesi.

💻 Kullanılan Teknolojiler Geliştirme Dili: C# (.NET Framework)

Arayüz: Windows Forms

Veritabanı: Microsoft SQL Server

Veri Erişimi: ADO.NET

Protokoller: SMTP (E-posta Servisleri)

📂 Veritabanı Yapısı Proje, ilişkisel bir veritabanı mimarisi üzerine kuruludur. Temel tablolar şunları içerir:

Reservations: Müşteri bilgileri, tarih, saat ve masa eşleşmeleri.

Tables: Masaların kapasite, konum ve anlık durum verileri.

Users: Personel rolleri, giriş bilgileri ve yetki seviyeleri.

Products: Menü içerikleri, fiyatlandırma ve görsel yolları.

⚙️ Kurulum ve Çalıştırma GitHub üzerinden projeyi yerel makinenize klonlayın.

App.config dosyasındaki SQL Server bağlantı dizesini (Connection String) kendi sunucu bilgilerinize göre güncelleyin.

Veritabanı şemasını oluşturmak için projeyle birlikte sunulan SQL scriptlerini SSMS üzerinden çalıştırın.

Visual Studio üzerinden projeyi derleyin ve çalıştırın.

👥 Geliştirici Ekibi Koray Bağış Cenker Akmaz Mert Temizcan Ensar Eyüp Kılıç
