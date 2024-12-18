using Microsoft.AspNetCore.Mvc;

namespace WebProgramlama.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Admin()
        {
            return View();
        }
    }
}
