using AudioPlayer.Client.Services;
using AudioPlayer.Data.Implementation;
using AudioPlayer.Data.Services;
using AudioPlayer.Models;
using AudioPlayer.Models.ApiRequestModels;
using AudioPlayer.Models.DataTable;
using Microsoft.AspNetCore.Mvc;
using TagLib;

namespace AudioPlayer.Controllers;

public class HomeController : Controller
{
    IWebHostEnvironment _appEnvironment;
    private EFAudioRepository _repository;
    private readonly AudiosService _audioServiceClient;
    private readonly AudioService _audioServiceData;
    
    public HomeController(IWebHostEnvironment appEnvironment, HttpClient httpClient)
    {
        var context = new ApplicationContext();
        _audioServiceClient = new AudiosService(httpClient);
        _appEnvironment = appEnvironment;
        _repository =  new EFAudioRepository(context);
        _audioServiceData = new AudioService(_repository);
    }    
    
    public IActionResult Index()
    {
        return View(_audioServiceData.GetAll());
    }
    
    [HttpPost]
    public async Task<IActionResult> Index(IFormFile uploadedFile)
    {
        if (uploadedFile != null)
        {
            var path = "/Audios/" + uploadedFile;
            var fullPath = _appEnvironment.WebRootPath + path;
            SaveFile(uploadedFile, fullPath);
            using (var file = TagLib.File.Create(fullPath))
            {
                if ((file.Properties.MediaTypes == MediaTypes.Audio) is false)
                {
                    throw new ArgumentException($"{file.Name} не является аудиофайлом");
                }

                var audio = new Audio() { Name = uploadedFile.FileName, Path = path, Duration = (int)decimal.Ceiling(Convert.ToDecimal(file.Properties.Duration.TotalSeconds))};
                _repository.Add(audio);
                _repository.CommitChanges();
            }
           
        }
        return await Task.FromResult<IActionResult>(RedirectToAction("Index"));
        
    }
    
    
    [HttpPost]
    [Route("[controller]/get")]
    public async Task<DataTableResponse<AudioViewModel>> GetAudiosAsync(DataTableRequest dataTableRequest)
    {
        var (orderColumnName, isAscending) = dataTableRequest.GetOrderColumn();

        var selectParameters = new AudiosSelectParameters(dataTableRequest.Start, dataTableRequest.Length,
            dataTableRequest.Search.Value, orderColumnName ?? "Name", isAscending);

        var selectedData = await _audioServiceClient.GetAudiosAsync(selectParameters);

        var dataTableResponse = new DataTableResponse<AudioViewModel>(dataTableRequest.Draw,
            selectedData.RecordsTotal, selectedData.RecordsFiltered, selectedData.Data);

        return dataTableResponse;
    }

    public async Task<IActionResult> DeleteAudio(int audioId)
    {
        var audio = await _audioServiceClient.GetAudioAsync(audioId);

        return PartialView("_DeleteAudioPartial", audio);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAudio(AudioViewModel audioViewModel)
    {
        var audio = await _audioServiceClient.GetAudioAsync(audioViewModel.NumberInPlayList);

        await _audioServiceClient.DeleteAudioAsync(audio);

        return PartialView("_DeleteAudioPartial", audio);
    }

    private void SaveFile(IFormFile uploadedFile, string fullPath)
    {
        using (var fileStream = new FileStream(fullPath, FileMode.Create))
        {
            uploadedFile.CopyTo(fileStream);
            fileStream.Dispose();
        }
    }
}