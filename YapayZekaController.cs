using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

public class YapayZekaController : Controller
{
    private readonly IAIService _aiService;
    public YapayZekaController(IAIService aiService)
    {
        _aiService = aiService;
    }

    // GET: /AI
    [HttpGet]
    public IActionResult YapayZeka()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ProcessPhoto(IFormFile photo, string userRequest)
    {
        if (photo == null || photo.Length == 0)
        {
            ViewData["Error"] = "Lütfen bir fotoğraf yükleyin.";
            return View("YapayZeka");
        }
        if (string.IsNullOrWhiteSpace(userRequest))
        {
            ViewData["Error"] = "Lütfen bir talep girin.";
            return View("YapayZeka");
        }
        try
        {
            var resultImageUrl = await _aiService.ProcessPhotoWithTextAsync(photo, userRequest);

            if (string.IsNullOrEmpty(resultImageUrl))
            {
                throw new Exception("Processed image URL is null or empty.");
            }

            ViewData["ResultImageUrl"] = resultImageUrl;
            return View("YapayZekaSonuc");
        }
        catch (Exception ex)
        {
            ViewData["Error"] = $"Bir hata oluştu: {ex.Message}";
            return View("YapayZeka");
        }
    } 
    public IActionResult YapayZekaSonuc()
    {
        return View(); 
    }
} 