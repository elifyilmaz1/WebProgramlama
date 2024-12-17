using Microsoft.AspNetCore.Mvc;

namespace WebProgramlama.Controllers
{
    public class YonetimController : Controller
    {
        public IActionResult AdminPanel()
        {
            return View();
        }
    }
}
