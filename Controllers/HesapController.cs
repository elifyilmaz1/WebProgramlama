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

       
            public IActionResult Calisanlar()
            {
                return View(); // Bu, Calisanlar.cshtml dosyasını döndürecektir.
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
                ModelState.AddModelError("", "E-posta veya şifre yanlış!");
                return View();
            }

            // E-posta ve şifre doğruysa oturum aç
            HttpContext.Session.SetString("MusteriEposta", Musteri.Eposta);
            HttpContext.Session.SetInt32("MusteriId", Musteri.Id);

            // Admin mi kontrol et
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

        
        }
    }
