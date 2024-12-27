using Microsoft.AspNetCore.Mvc;
using WebProgramlama.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace WebProgramlama.Controllers
{
    public class RandevuViewController : Controller
    {
        private readonly UygulamaDbContext _dbContext;

        public RandevuViewController(UygulamaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Randevu()
        {
            // Çalışan ve hizmet verilerini ViewBag'e ekliyoruz
            ViewBag.Calisan = _dbContext.Calisan.ToList();
            ViewBag.Hizmet = _dbContext.Hizmet.ToList();
            return View("Randevu");
        }

        [HttpPost]
        public IActionResult Randevu(int CalisanId, DateTime RandevuTarihi, TimeSpan BaslangicSaati, int HizmetId)
        {
            // Oturumdan müşteri ID'si alıyoruz
            var MusteriId = HttpContext.Session.GetInt32("MusteriId");

            if (MusteriId == null)
            {
                // Müşteri oturumu yoksa giriş sayfasına yönlendiriyoruz
                ModelState.AddModelError("", "Oturum açmanız gerekiyor.");
                return RedirectToAction("Giris", "Hesap");
            }

            // Veritabanından çalışan, müşteri ve hizmet bilgilerini alıyoruz
            var Calisan = _dbContext.Calisan.FirstOrDefault(c => c.Id == CalisanId);
            var Musteri = _dbContext.Musteri.FirstOrDefault(m => m.Id == MusteriId.Value);
            var Hizmet = _dbContext.Hizmet.FirstOrDefault(h => h.Id == HizmetId);

            if (Calisan == null || Hizmet == null || Musteri == null)
            {
                // Geçersiz seçim durumunda hata mesajı veriyoruz
                ModelState.AddModelError("", "Geçersiz seçim yapıldı.");
                return View();
            }

            // Aynı çalışan için aynı saatte başka bir randevu olup olmadığını kontrol ediyoruz
            var mevcutRandevu = _dbContext.Randevu.FirstOrDefault(r => r.CalisanId == CalisanId &&
                                                              r.RandevuTarihi.Date == RandevuTarihi.Date &&
                                                              r.BaslangicSaati == BaslangicSaati);
            if (mevcutRandevu != null)
            {
                ModelState.AddModelError("", "Seçtiğiniz saatte bu çalışanın zaten bir randevusu var.");
                return View();
            }

            // Yeni randevuyu oluşturuyoruz
            var yeniRandevu = new Randevu
            {
                Calisan = Calisan,
                RandevuTarihi = RandevuTarihi,
                BaslangicSaati = BaslangicSaati,
                Musteri = Musteri,
                Hizmet = Hizmet
            };

            // Randevuyu veritabanına ekliyoruz
            _dbContext.Randevu.Add(yeniRandevu);
            _dbContext.SaveChanges();

            // Başarı mesajı ile yönlendirme yapıyoruz
            TempData["SuccessMessage"] = "Randevunuz başarıyla alınmıştır.";
            return RedirectToAction("RandevuOnay");

        }

        public IActionResult RandevuOnay()
        {
            return View();
        }
    }
}
