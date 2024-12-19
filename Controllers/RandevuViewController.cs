using Microsoft.AspNetCore.Mvc;
using WebProgramlama.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebProgramlama.Controllers
{
    public class RandevuViewController : Controller
    {
        private readonly UygulamaDbContext _context;
        public RandevuViewController(UygulamaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Randevu()
        {
            ViewBag.Calisan= _context.Calisan.ToList();
            ViewBag.Hizmet= _context.Hizmet.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Randevu(Randevu Randevu)
        {
            if (ModelState.IsValid)
            {
                var uygunlukKontrol = await _context.Randevu.AnyAsync(r =>
                        r.CalisanId == Randevu.CalisanId &&
                        r.RandevuTarihi == Randevu.RandevuTarihi &&
                        r.BaslangicSaati == Randevu.BaslangicSaati);
                if (uygunlukKontrol)
                {
                    ModelState.AddModelError("", "Bu saat için seçilen çalışan uygun değil.");
                    ViewBag.Calisan = _context.Calisan.ToList();
                    ViewBag.Hizmet= _context.Hizmet.ToList();
                    return View(Randevu);
                }

                _context.Randevu.Add(Randevu);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Randevu talebiniz alınmıştır.";

                return RedirectToAction("Randevu", "Hesap");
            }
            ViewBag.Calisan = _context.Calisan.ToList();
            ViewBag.Hizmet = _context.Hizmet.ToList();
            return View(Randevu);
        }
    }
}