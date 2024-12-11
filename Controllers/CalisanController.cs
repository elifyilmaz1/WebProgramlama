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
            var mevcutCalisan =await _dbContext.Calisan.FirstOrDefaultAsync(c => c.Id == id);
            if (mevcutCalisan == null)
            {
                return NotFound("Çalışan ID bulunamadı.");
            }

            mevcutCalisan.Isim = guncelCalisan.Isim;
            mevcutCalisan.Gorev = guncelCalisan.Gorev;
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
    }
}
