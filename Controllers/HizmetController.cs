using Microsoft.AspNetCore.Mvc;
using WebProgramlama.Models;

[Route("api/[controller]")]
[ApiController]
public class HizmetController : ControllerBase
{
    private readonly UygulamaDbContext _dbContext;

    public HizmetController(UygulamaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Hizmetleri listeleme
    [HttpGet]
    public ActionResult GetAll()
    {
        var Hizmet = _dbContext.Hizmet.ToList();
        return Ok(Hizmet);
    }

    // Yeni bir hizmet ekleme
    [HttpPost]
    public IActionResult AddHizmet([FromBody] Hizmet yeniHizmet)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Hizmet.Add(yeniHizmet);
            _dbContext.SaveChanges();
            return Ok(yeniHizmet);
        }
        return BadRequest(ModelState);
    }
}
