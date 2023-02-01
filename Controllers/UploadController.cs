using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Task4.Models;

public class UploadController : Controller
{
    private readonly FileUploadService _fileUploadService;

    public UploadController(FileUploadService fileUploadService)
    {
        _fileUploadService = fileUploadService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file, string textData)
    {
        var id = await _fileUploadService.UploadFile(file, textData);

        return RedirectToAction("Index", "View", new { id });
    }
}
