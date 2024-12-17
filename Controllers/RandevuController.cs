using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProgramlama.Models;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRandevuById(int id)
        {
            var randevu = await _dbContext.Randevu
                .Include(r => r.Calisan)
                .Include(r => r.Musteri)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (randevu == null)
            {
                return NotFound("Randevu bulunamadı.");
            }

            return Ok(randevu);
        }
        [HttpPost]
        public async Task<IActionResult> AddRandevu([FromBody] Randevu yeniRandevu)
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
            
            _dbContext.Randevu.Add(yeniRandevu);
            await _dbContext.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetAllRandevu), new { id = yeniRandevu.Id }, yeniRandevu);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRandevu(int id, [FromBody] Randevu guncelRandevu)
        {
            if (id != guncelRandevu.Id)
            {
                return BadRequest("Id uyuşmuyor.");
            }

            var mevcutRandevu = await _dbContext.Randevu.FindAsync(id);

            if (mevcutRandevu == null)
            {
                return NotFound("Randevu bulunamadı.");
            }


            mevcutRandevu.RandevuTarihi = guncelRandevu.RandevuTarihi;
            mevcutRandevu.CalisanId = guncelRandevu.CalisanId;
            mevcutRandevu.MusteriId = guncelRandevu.MusteriId;

            await _dbContext.SaveChangesAsync();

            return Ok(mevcutRandevu);
        }

        [HttpDelete("{id}")]
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
