using Microsoft.AspNetCore.Mvc;
using WebProgramlama.Models;

[Route("api/[controller]")]
[ApiController]
public class MusteriController : ControllerBase
{
    private readonly UygulamaDbContext _dbContext;

    public MusteriController(UygulamaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Müşterileri listeleme
    [HttpGet]
    public ActionResult GetAll()
    {
        var Musteri = _dbContext.Musteri.ToList();
        return Ok(Musteri);
    }

    // Yeni bir müşteri ekleme
    [HttpPost]
    public IActionResult AddMusteri([FromBody] Musteri yeniMusteri)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Musteri.Add(yeniMusteri);
            _dbContext.SaveChanges();
            return Ok(yeniMusteri);
        }
        return BadRequest(ModelState);
    }
}
