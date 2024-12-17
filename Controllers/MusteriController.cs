using Microsoft.AspNetCore.Mvc;
using WebProgramlama.Models;
using BCrypt.Net;

[Route("api/[controller]")]
[ApiController]
public class MusteriController : ControllerBase
{
    private readonly UygulamaDbContext _dbContext;

    public MusteriController(UygulamaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] Musteri loginMusteri)
    {
        // Admin için sabit kullanıcı adı ve şifre kontrolü
        if (loginMusteri.Eposta == "B221210031@sakarya.edu.tr" &&
            BCrypt.Net.BCrypt.Verify(loginMusteri.Sifre, "sau"))
        {
            return Ok(new { message = "Admin girişi başarılı" });
        }

        var musteri = _dbContext.Musteri.FirstOrDefault(m => m.Eposta == loginMusteri.Eposta);
        if (musteri == null)
        {
            return Unauthorized("Kullanıcı bulunamadı");
        }

        if (!BCrypt.Net.BCrypt.Verify(loginMusteri.Sifre, musteri.Sifre))
        {
            return Unauthorized("Şifre hatalı");
        }

        return Ok(new { message = "Giriş başarılı", rol = musteri.Rol });
    }

    [HttpPost]
    public IActionResult AddMusteri([FromBody] Musteri yeniMusteri)
    {
        if (ModelState.IsValid)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(yeniMusteri.Sifre);
            yeniMusteri.Sifre = hashedPassword;

            if (yeniMusteri.Eposta == "B221210031@sakarya.edu.tr")
            {
                yeniMusteri.Rol = "Admin";
            }
            else
            {
                yeniMusteri.Rol = "Uye";
            }

            _dbContext.Musteri.Add(yeniMusteri);
            _dbContext.SaveChanges();
            return Ok(yeniMusteri);
        }
        return BadRequest(ModelState);
    }
}
