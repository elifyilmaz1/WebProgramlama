using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProgramlama.Models;

[Route("api/[controller]")]
[ApiController]
public class RandevuController : ControllerBase
{
    private readonly UygulamaDbContext _dbContext;

    public RandevuController(UygulamaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Randevuları listeleme
    [HttpGet]
    public ActionResult GetAll()
    {
        var Randevu = _dbContext.Randevu.Include(r => r.Calisan).Include(r => r.Musteri).ToList();
        return Ok(Randevu);
    }

    // Yeni bir randevu ekleme
    [HttpPost]
    public IActionResult AddRandevu([FromBody] Randevu yeniRandevu)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Randevu.Add(yeniRandevu);
            _dbContext.SaveChanges();
            return Ok(yeniRandevu);
        }
        return BadRequest(ModelState);
    }
}
