using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Task4.Models;

public class ViewController : Controller
{
    private readonly FileUploadService _fileUploadService;

    public ViewController(FileUploadService fileUploadService)
    {
        _fileUploadService = fileUploadService;
    }

    public async Task<IActionResult> Index(string id)
    {
        var imageData = await _fileUploadService.GetImageData(id);

        return View(imageData);
    }

    public IActionResult Edit(string id)
    {
        return View(id);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, IFormFile file, string textData)
    {
        await _fileUploadService.UpdateImageData(id, file, textData);
        return RedirectToAction("Index", new { id });
    }
}
