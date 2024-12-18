using Microsoft.AspNetCore.Mvc;
using WebProgramlama.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace WebProgramlama.Controllers
{
    public class HesapController : Controller
    {
        private readonly UygulamaDbContext _dbContext;

        public HesapController(UygulamaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Giriş Sayfası
        [HttpGet]
        public IActionResult Giris()
        {
            return View();
        }

        // Kayıt Sayfası
        [HttpGet]
        public IActionResult Kayit()
        {
            return View();
        }

        // Randevu Sayfası
        [HttpGet]
        public IActionResult Randevu()
        {
            ViewBag.Calisan = _dbContext.Calisan.ToList();
            ViewBag.Hizmet = _dbContext.Hizmet.ToList();
            return View();
        }

        // Kayıt İşlemi
        [HttpPost]
        public IActionResult Kayit(string IsimSoyisim, string IletisimNumarasi, string Eposta, string Sifre, string TekrarSifre)
        {
            if (Sifre != TekrarSifre)
            {
                ModelState.AddModelError("", "Şifreler uyuşmuyor.");
                return View();
            }

            // Aynı e-posta ile kayıtlı müşteri kontrolü
            var mevcutMusteri = _dbContext.Musteri.FirstOrDefault(m => m.Eposta == Eposta);
            if (mevcutMusteri != null)
            {
                ModelState.AddModelError("", "Bu e-posta adresi zaten kullanılıyor.");
                return View();
            }

            // Şifreyi bcrypt ile şifrele
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Sifre);

            // Rol belirleme
            string Rol = Eposta == "B221210031@sakarya.edu.tr" ? "Admin" : "Uye";

            var yeniMusteri = new Musteri
            {
                IsimSoyisim = IsimSoyisim,
                IletisimNumarasi = IletisimNumarasi,
                Eposta = Eposta,
                Sifre = hashedPassword,
                Rol = Rol,
                Randevu = new List<Randevu>()
            };

            _dbContext.Musteri.Add(yeniMusteri);
            _dbContext.SaveChanges();

            return RedirectToAction("Giris");
        }

        // Giriş İşlemi
        [HttpPost]
        public IActionResult Giris(string Eposta, string Sifre)
        {
            // Kullanıcıyı e-posta ile arama
            var Musteri = _dbContext.Musteri.FirstOrDefault(m => m.Eposta == Eposta);

            // E-posta bulunamazsa ya da şifre yanlışsa hata mesajı
            if (Musteri == null || !BCrypt.Net.BCrypt.Verify(Sifre, Musteri.Sifre))
            {
                ModelState.AddModelError("", "E-posta veya şifre hatalı.");
                return View();
            }

            // Oturum açan kullanıcı bilgilerini session'a kaydet
            HttpContext.Session.SetString("MusteriEposta", Musteri.Eposta);
            HttpContext.Session.SetInt32("MusteriId", Musteri.Id);

            // Admin ise admin sayfasına yönlendir, değilse randevu sayfasına
            if (Musteri.Rol == "Admin")
            {
                return RedirectToAction("Admin", "Admin");
            }

            return RedirectToAction("Randevu", "Hesap");
        }

        // Çıkış İşlemi
        public IActionResult Cikis()
        {
            HttpContext.Session.Clear(); // Oturumu temizle
            return RedirectToAction("Giris");
        }

        // Randevu İşlemi
        [HttpPost]
        public IActionResult Randevu(int CalisanId, DateTime RandevuTarihi, TimeSpan BaslangicSaati, int HizmetId)
        {
            // Çalışan ve hizmet listelerini görüntüle
            ViewBag.Calisan = _dbContext.Calisan.ToList();
            ViewBag.Hizmet = _dbContext.Hizmet.ToList();

            // Seçilen çalışan, hizmet ve müşteri bilgilerini al
            var Calisan = _dbContext.Calisan.FirstOrDefault(c => c.Id == CalisanId);
            var Musteri = _dbContext.Musteri.FirstOrDefault(m => m.Id == HttpContext.Session.GetInt32("MusteriId").Value);
            var Hizmet = _dbContext.Hizmet.FirstOrDefault(h => h.Id == HizmetId);

            // Geçersiz çalışan veya hizmet seçimi durumunda hata mesajı
            if (Calisan == null)
            {
                ModelState.AddModelError("", "Geçersiz çalışan seçimi.");
                return View();
            }

            if (Hizmet == null)
            {
                ModelState.AddModelError("", "Geçersiz hizmet seçimi.");
                return View();
            }

            // Geçmiş tarihe randevu alma durumunda hata mesajı
            if (RandevuTarihi.Date < DateTime.UtcNow.Date ||
                (RandevuTarihi.Date == DateTime.UtcNow.Date && BaslangicSaati < DateTime.UtcNow.TimeOfDay))
            {
                ModelState.AddModelError("", "Geçmiş bir tarihe randevu oluşturamazsınız.");
                return View();
            }

            // Yeni randevu oluştur ve veritabanına kaydet
            var yeniRandevu = new Randevu
            {
                CalisanId = CalisanId,
                RandevuTarihi = RandevuTarihi,
                BaslangicSaati = BaslangicSaati,
                MusteriId = Musteri.Id,
                Calisan = Calisan,
                HizmetId = HizmetId,
                Hizmet = Hizmet,
                Musteri = Musteri
            };

            _dbContext.Randevu.Add(yeniRandevu);
            _dbContext.SaveChanges();

            // Randevu onay sayfasına yönlendir
            return RedirectToAction("RandevuOnay");
        }

        // Randevu onay sayfası
        [HttpGet]
        public IActionResult RandevuOnay()
        {
            return View();
        }
    }
}
