using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProgramlama.Models;
using System.Linq;
using System.Threading.Tasks;
using WebProgramlama.ViewModels;
using System;

namespace WebProgramlama.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandevuController : ControllerBase
    {
        private readonly UygulamaDbContext _dbContext;

        public RandevuController(UygulamaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]  
        public async Task<IActionResult> GetAllRandevu()
        {
            var Randevu = await _dbContext.Randevu
                .Include(r => r.Calisan)
                .Include(r => r.Musteri)
                .ToListAsync();

            return Ok(Randevu);
        }

        [HttpGet("{id}")] // Randevuyu ID'ye göre getir
        public async Task<IActionResult> GetRandevuById(int id)
        {
            var randevu = await _dbContext.Randevu
                .FirstOrDefaultAsync(r => r.Id == id);


            if (randevu == null)
            {
                return NotFound("Randevu bulunamadı.");
            }


            var musteri = await _dbContext.Musteri
    .Where(m => m.Id == randevu.MusteriId)
    .Select(m => new
    {
        m.Id,
        m.IsimSoyisim,
        m.IletisimNumarasi,
        m.Eposta
    })
    .FirstOrDefaultAsync();

            var calisan = await _dbContext.Calisan
                                .Where(m => m.Id == randevu.CalisanId)
                                .Select(m => new
                                {
                                    m.Id,
                                    m.Isim,
                                    m.UzmanlikAlani,
                                    m.YapabildigiIslemler,
                                    m.SaatlikUcret,
                                    m.CalismaSaatleri
                                })
                                .FirstOrDefaultAsync();
            var hizmet = await _dbContext.Hizmet
                .Where(h => h.Id == randevu.HizmetId)
                .Select(h => new
                {
                    h.Id,
                    h.Isim,
                    h.Ucret
                })
                .FirstOrDefaultAsync();

            if (musteri == null || calisan == null || hizmet == null)
            {
                return NotFound("Randevu ile ilişkili veriler bulunamadı.");
            }

            var gosterilecekRandevu = new
            {
                randevu.Id,
                randevu.RandevuTarihi,
                randevu.BaslangicSaati,
                randevu.Ucret,
                Musteri = musteri,
                Calisan = calisan,
                Hizmet = hizmet
            };

            return Ok(gosterilecekRandevu);
        }

        [HttpPost] // Yeni randevu oluştur
        public async Task<IActionResult> AddRandevu([FromBody] RandevuOlustur yeniRandevu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var uygunlukKontrol = await _dbContext.Randevu.AnyAsync(r =>
                r.CalisanId == yeniRandevu.CalisanId &&
                r.RandevuTarihi == yeniRandevu.RandevuTarihi);

            if (uygunlukKontrol)
            {
                return BadRequest("Bu saat için seçilen çalışan uygun değil.");
            }
            int musteriId = HttpContext.Session.GetInt32("MusteriId")!.Value;
            var ucret = _dbContext.Hizmet.FirstOrDefault(h => h.Id == yeniRandevu.HizmetId)!.Ucret;

            DateTime randevuTarihi = yeniRandevu.RandevuTarihi;
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            randevuTarihi = TimeZoneInfo.ConvertTime(randevuTarihi, timeZoneInfo); // Zaman dilimini belirt
            Randevu eklenecekRandevu = new Randevu
            {
                CalisanId = yeniRandevu.CalisanId,
                MusteriId = musteriId,
                HizmetId = yeniRandevu.HizmetId,
                RandevuTarihi = randevuTarihi,
                BaslangicSaati = yeniRandevu.BaslangicSaati, // TimeSpan olarak atanıyor
                Ucret = ucret,
            };

            try
            {
                _dbContext.Randevu.Add(eklenecekRandevu);
                await _dbContext.SaveChangesAsync();

                // Randevu oluşturulduktan sonra ilişkili verileri yüklemek
                var randevu = await _dbContext.Randevu
                    .Include(r => r.Calisan)   // Calisan'ı yükle
                    .Include(r => r.Musteri)   // Musteri'yi yükle
                    .Include(r => r.Hizmet)    // Hizmet'i yükle
                    .FirstOrDefaultAsync(r => r.Id == eklenecekRandevu.Id);

                if (randevu == null)
                {
                    return NotFound("Randevu bulunamadı.");
                }

                return CreatedAtAction(nameof(GetRandevuById), new { id = randevu.Id }, randevu);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Randevu eklenirken bir hata oluştu." +  ex.Message);
            }
            
        }

        [HttpDelete("{id}")] // Randevuyu sil
        public async Task<IActionResult> DeleteRandevu(int id)
        {
            var randevu = await _dbContext.Randevu.FindAsync(id);
            if (randevu == null)
            {
                return NotFound("Randevu bulunamadı.");
            }

            _dbContext.Randevu.Remove(randevu);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
