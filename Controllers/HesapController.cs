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

        [HttpGet]
        public IActionResult Giris()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Kayit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Randevu()
        {
            var Calisan = _dbContext.Calisan.ToList();
            ViewBag.Calisan = Calisan;
            return View();
        }

        [HttpPost]
        public IActionResult Kayit(string IsimSoyisim,string IletisimNumarasi, string Eposta, string Sifre, string TekrarSifre)
        {
            if (Sifre != TekrarSifre)
            {
                ModelState.AddModelError("", "Şifreler uyuşmuyor.");
                return View();
            }

            var mevcutMusteri = _dbContext.Musteri.FirstOrDefault(m => m.Eposta == Eposta);
            if (mevcutMusteri != null)
            {
                ModelState.AddModelError("", "Bu e-posta adresi zaten kullanılıyor.");
                return View();
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Sifre);

            string rol = Eposta == " B221210031@sakarya.edu.tr" ? "Admin" : "Uye";


            var yeniMusteri = new Musteri
            {
                IsimSoyisim = IsimSoyisim,
                IletisimNumarasi = Convert.ToString(IletisimNumarasi),
                Eposta = Eposta,
                Sifre = hashedPassword,
                Rol = rol,
                Randevu = new List<Randevu>()
            };

            _dbContext.Musteri.Add(yeniMusteri);
            _dbContext.SaveChanges();

            return RedirectToAction("Giris");
        }

        [HttpPost]
        public IActionResult Giris(string Eposta, string Sifre)
        {
            var Musteri = _dbContext.Musteri.FirstOrDefault(m => m.Eposta == Eposta);
            if (Musteri == null || !BCrypt.Net.BCrypt.Verify(Sifre, Musteri.Sifre))
            {
                ModelState.AddModelError("", "E-posta veya şifre hatalı.");
                return View();
            }
            HttpContext.Session.SetString("MusteriEposta", Musteri.Eposta);
            HttpContext.Session.SetInt32("MusteriId", Musteri.Id);
            
            if (Musteri.Rol == "Admin")
            {
                 return RedirectToAction("AdminPanel", "Yonetim");
            }
            
            return RedirectToAction("Randevu");


        }
        public IActionResult Cikis()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Giris");
        }


        [HttpPost]
        public IActionResult Randevu(int CalisanId, DateTime RandevuTarihi,TimeSpan BaslangicSaati)
        {
            var Calisan = _dbContext.Calisan.FirstOrDefault(c => c.Id == CalisanId);
            var Musteri = _dbContext.Musteri.FirstOrDefault(m => m.Id == HttpContext.Session.GetInt32("UserId").Value);


            if (Calisan == null)
            {
                ModelState.AddModelError("", "Geçersiz çalışan seçimi.");
                return View();
            }
        if (RandevuTarihi.Date < DateTime.Today || 
           (RandevuTarihi.Date == DateTime.Today && BaslangicSaati < DateTime.Now.TimeOfDay))
        {
            ModelState.AddModelError("", "Geçmiş bir tarihe randevu oluşturamazsınız.");
            return View();
        }

            var yeniRandevu = new Randevu
            {
                CalisanId = CalisanId,
                RandevuTarihi = RandevuTarihi,
                BaslangicSaati = BaslangicSaati,
                MusteriId = HttpContext.Session.GetInt32("UserId").Value,
                Calisan = Calisan,
                Hizmet = _dbContext.Hizmet.FirstOrDefault(),
                Musteri = Musteri
            };

            _dbContext.Randevu.Add(yeniRandevu);
            _dbContext.SaveChanges();

            return RedirectToAction("RandevuOnay");  
        }

    }
}
