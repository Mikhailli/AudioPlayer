using System.Text;
using AudioPlayer.Models.ApiRequestModels;
using AudioPlayer.Models.ApiResponseModel;
using Newtonsoft.Json;

namespace AudioPlayer.Client.Services;

public class AudiosService
{
    private readonly HttpClient _httpClient;
    
    public AudiosService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<SelectedData<AudioViewModel>> GetAudiosAsync(
        AudiosSelectParameters? parameters = null)
    {
        var requestUrl = Api.Audios.GetAudios("https://localhost:44353", parameters);

        var response = await _httpClient.GetAsync(requestUrl);

        var selectedAudios = await response.Content.ReadAsAsync<SelectedData<AudioViewModel>>();
        
        return selectedAudios;
    }

    public async Task<AudioViewModel> GetAudioAsync(int audioId)
    {
        var requestUrl = Api.Audios.GetAudio("https://localhost:44353", audioId);
        var response = await _httpClient.GetAsync(requestUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Ошибка получения данных о аудио из {requestUrl} (код: {response.StatusCode}).");
        }
        
        var audio = await response.Content.ReadAsAsync<AudioViewModel>();
        return audio;
    }

    public async Task UpdateAudioAsync(AudioViewModel audioViewModel)
    {
        var requestUrl = Api.Audios.UpdateAudio("https://localhost:44353", audioViewModel.NumberInPlayList);

        var jsonString = JsonConvert.SerializeObject(audioViewModel);
        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(requestUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Ошибка обновления данных о аудио из {requestUrl} (код: {response.StatusCode}).");
        }
    }

    public async Task DeleteAudioAsync(AudioViewModel audio)
    {
        var requestUrl = Api.Audios.DeleteAudio("https://localhost:44353", audio.NumberInPlayList);

        await _httpClient.DeleteAsync(requestUrl);
    }
}