#nullable enable
using AudioPlayer.Data.Implementation;
using AudioPlayer.Models;
using Microsoft.AspNetCore.Mvc;
using TagLib;

namespace AudioPlayer.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationContext _db;
    IWebHostEnvironment _appEnvironment;
    private EFAudioRepository _repository;
    
    public HomeController(IWebHostEnvironment appEnvironment)
    {
        _appEnvironment = appEnvironment;
        _db = new ApplicationContext();
        _repository =  new EFAudioRepository(_db);
    }    
    
    public IActionResult Index()
    {
        var context = new ApplicationContext();
        var repo = new EFAudioRepository(context);

        return View(repo.GetAll().ToList());
    }
    
    [HttpPost]
    public async Task<IActionResult> Index(IFormFile uploadedFile)
    {
        if (uploadedFile != null)
        {
            var path = "/Audios/" + uploadedFile.FileName;
            var fullPath = _appEnvironment.WebRootPath + path;

            using (var file = TagLib.File.Create(fullPath))
            {
                if ((file.Properties.MediaTypes == MediaTypes.Audio) is false)
                {
                    throw new ArgumentException($"{file.Name} не является аудиофайлом");
                }
                
                SaveFile(uploadedFile, fullPath);

                var audio = new Audio() { Name = uploadedFile.FileName, Path = path, Duration = (int)decimal.Ceiling(Convert.ToDecimal(file.Properties.Duration.TotalSeconds))};
                _repository.Add(audio);
                _repository.CommitChanges();
            }
           
        }
            
        return await Task.FromResult<IActionResult>(RedirectToAction("Index"));
    }

    private async void SaveFile(IFormFile uploadedFile, string fullPath)
    {
        await using (var fileStream = new FileStream(fullPath, FileMode.Create))
        {
            await uploadedFile.CopyToAsync(fileStream);
        }
    }
}