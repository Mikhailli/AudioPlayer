using AudioPlayer.Data.Implementation;
using AudioPlayer.Data.Services;
using AudioPlayer.Models;
using AudioPlayer.Models.ApiRequestModels;
using Microsoft.AspNetCore.Mvc;
using AudiosSelectParameters = AudioPlayer.Data.Domain.SelectionModels.AudiosSelectParameters;

namespace AudioPlayer.ApiController;

[ApiController]
[Route("api/[controller]")]
public class HomeController : Controller
{
    private readonly AudioService _audiosService;

    public HomeController(HttpClient httpClient)
    {
        _audiosService = new AudioService(new EFAudioRepository(new ApplicationContext()));
    }

    [HttpGet]
    [Route("get")]
    public IActionResult GetAudios([FromQuery] AudiosSelectRequestModel selectRequestModel)
    {
        var selectParameters = new AudiosSelectParameters(selectRequestModel.Start, selectRequestModel.Length,
            selectRequestModel.SearchPattern, selectRequestModel.OrderColumnName, selectRequestModel.IsAscending);

        var audios = _audiosService.SelectAudios(selectParameters);

        return Ok(audios);
    }

    [HttpGet]
    [Route("{audioId:int}")]
    public IActionResult GetAudio(int audioId)
    {
        var audio = _audiosService.GetAudioById(audioId);

        if (audio is null)
        {
            return NotFound();
        }

        return Json(audio);
    }

    [HttpDelete]
    [Route("{audioId}")]
    public IActionResult DeleteAudioData(int audioId)
    {
        var audio = _audiosService.GetAudioById(audioId);

        if (audio is null)
        {
            return NotFound();
        }
        
        _audiosService.DeleteAudio(audio);

        return Ok();
    }
}