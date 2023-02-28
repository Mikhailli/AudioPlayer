using AudioPlayer.Models.ApiRequestModels;
using AudioPlayer.Models.ApiResponseModel;

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

        if (response.IsSuccessStatusCode)
        {
            var audio = await response.Content.ReadAsAsync<AudioViewModel>();
            return audio;
        }
        else
        {
            var responseString = response.Content.ReadAsStringAsync().Result;
            throw new Exception($"Ошибка получения данных о аудио из {requestUrl} (код: {response.StatusCode}).");
        }
    }

    public async Task DeleteAudioAsync(AudioViewModel audio)
    {
        var requestUrl = Api.Audios.DeleteAudio("https://localhost:44353", audio.NumberInPlayList);

        var response = await _httpClient.DeleteAsync(requestUrl);
    }
}