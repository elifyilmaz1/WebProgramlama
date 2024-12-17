using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebProgramlama.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebProgramlama.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalisanController : ControllerBase
    {
        private readonly UygulamaDbContext _dbContext;

        public CalisanController(UygulamaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var Calisan = _dbContext.Calisan.ToList();
            return Ok(Calisan);

        }
        [HttpGet("Randevu/{CalisanId}")]
        public async Task<IActionResult> GetRandevu(int calisanId)
        {
            var Randevu = await _dbContext.Randevu
                                             .Where(r => r.CalisanId == calisanId)
                                             .ToListAsync();

            if (!Randevu.Any())
            {
                return NotFound("Bu çalışanın randevuları bulunamadı.");
            }

            return Ok(Randevu);
        }

        [HttpPost("Randevu")]
        public async Task<IActionResult> AddRandevu([FromBody] Randevu yeniRandevu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bitisSaati = yeniRandevu.BaslangicSaati + TimeSpan.FromHours(1);

            var uygunlukKontrol = await _dbContext.Randevu.AnyAsync(r =>
                r.CalisanId == yeniRandevu.CalisanId &&
                r.RandevuTarihi.Date == yeniRandevu.RandevuTarihi.Date &&
                (r.BaslangicSaati < bitisSaati && (r.BaslangicSaati + TimeSpan.FromHours(1)) > yeniRandevu.BaslangicSaati));

            if (uygunlukKontrol)
            {
                return BadRequest("Bu saatte çalışan zaten bir işte meşgul.");
            }

            _dbContext.Randevu.Add(yeniRandevu);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllRandevu), new { id = yeniRandevu.Id }, yeniRandevu);
        }

        private object GetAllRandevu()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> AddCalisan([FromBody] Calisan yeniCalisan)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.Calisan.AddAsync(yeniCalisan);
                await _dbContext.SaveChangesAsync();
                return Ok(yeniCalisan);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCalisan(int id, [FromBody] Calisan guncelCalisan)
        {
            var mevcutCalisan = await _dbContext.Calisan.FirstOrDefaultAsync(c => c.Id == id);
            if (mevcutCalisan == null)
            {
                return NotFound("Çalışan ID bulunamadı.");
            }

            mevcutCalisan.Isim = guncelCalisan.Isim;
            mevcutCalisan.SaatlikUcret = guncelCalisan.SaatlikUcret;



            await _dbContext.SaveChangesAsync();
            return Ok(mevcutCalisan);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalisan(int id)
        {
            var silinecekCalisan = await _dbContext.Calisan.FirstOrDefaultAsync(c => c.Id == id);
            if (silinecekCalisan == null)
            {
                return NotFound("Çalışan ID bulunamadı.");
            }

            _dbContext.Calisan.Remove(silinecekCalisan);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    
    [HttpDelete("Randevu/{id}")]
        public async Task<IActionResult> DeleteRandevu(int id)
        {
            var Randevu = await _dbContext.Randevu.FirstOrDefaultAsync(r => r.Id == id);

            if (Randevu == null)
            {
                return NotFound("Randevu bulunamadı.");
            }

            _dbContext.Randevu.Remove(Randevu);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

    } }
