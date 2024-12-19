using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;

namespace WebProgramlama.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult HashPassword()
        {
            string[] plainPasswords = { "sau", "ay123", "ny123", "gc123" };
            var hashedPasswords = new List<string>();

            foreach (var plainPassword in plainPasswords)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);
                hashedPasswords.Add($"Düz Şifre: {plainPassword} | Hashlenmiş Şifre: {hashedPassword}");
            }

            return View(hashedPasswords);
        }
    }
}
