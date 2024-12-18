using Microsoft.AspNetCore.Mvc;
using WebProgramlama.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult RandevuOlustur()
        {
            ViewData["Calisan"] = _context.Calisan.ToList();
            ViewData["Hizmet"] = _context.Hizmet.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RandevuOlustur(Randevu Randevu)
        {
            if (ModelState.IsValid)
            {
                _context.Randevu.Add(Randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction("RandevuListele");
            }
            return View(Randevu);
        }

        [HttpGet]
        public IActionResult RandevuListele()
        {
            var Randevu= _context.Randevu
                .Include(r => r.Calisan)
                .Include(r=>r.Hizmet)
                .Include(r => r.Musteri)
                .ToList();
            return View(Randevu);
        }
    }
}
