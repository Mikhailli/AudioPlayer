using AudioPlayer.Data.Domain.SelectionModels;
using AudioPlayer.Data.Implementation;
using AudioPlayer.Models;
using AudioPlayer.Models.ApiResponseModel;

namespace AudioPlayer.Data.Services;

public class AudioService
{
    private readonly EFAudioRepository _repository;

    public AudioService(EFAudioRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public List<AudioViewModel> GetAll()
    {
        return ConvertToAudioViewModels(_repository.GetAll().ToList());
    }

    public SelectedData<AudioViewModel> SelectAudios(AudiosSelectParameters parameters)
    {
        var recordsTotal = _repository.GetCount();
        var filteredRecordsTotal = _repository.GetCount(parameters);

        var audios = _repository.GetAll().ToList();
        var audioViewModels = ConvertToAudioViewModels(_repository.GetAll(parameters, true));

        for (var i = 0; i < audios.Count; i++)
        {
            foreach (var audio in audioViewModels.Where(audio => audios[i].Id == audio.Id))
            {
                audio.NumberInPlayList = i + 1;
                break;
            }
        }

        return new SelectedData<AudioViewModel>(recordsTotal, filteredRecordsTotal, audioViewModels);
    }

    public AudioViewModel GetAudioById(int audioId)
    {
        return GetAll().First(audio => audio.NumberInPlayList == audioId);
    }

    public void DeleteAudio(AudioViewModel audio)
    {
        var audioToDelete = _repository.GetAll().First(entity => entity.Id == audio.Id);
        _repository.Delete(audioToDelete);
        _repository.CommitChanges();
    }

    public string ReturnsAudioNameWithoutExtension(string audioName)
    {
        ArgumentNullException.ThrowIfNull(audioName);
        
        return audioName.LastIndexOf('.') switch
        {
            -1 => audioName,
            _ => audioName[..audioName.LastIndexOf('.')]
        };
    }

    private string ConvertToMinutesColonSeconds(int duration)
    {
        if (duration <= 0)
        {
            throw new ArgumentException($"{duration} должна быть больше нуля");
        }

        if (duration / 60 == 0)
        {
            return $"00:{duration % 60}";
        }

        var minutes = duration / 60 < 10 ? $"0{duration / 60}" : $"{duration / 60}";
        var seconds = duration % 60 < 10 ? $"0{duration % 60}" : $"{duration % 60}";

        return $"{minutes}:{seconds}";
    }
    
    private List<AudioViewModel> ConvertToAudioViewModels(List<Audio> audios)
    {
        return audios.Select((audio, i) => new AudioViewModel
            {
                Id = audio.Id,
                NumberInPlayList = i + 1,
                Name = ReturnsAudioNameWithoutExtension(audio.Name),
                NameWithExtension = audio.Name,
                Path = audio.Path,
                Duration = ConvertToMinutesColonSeconds(audio.Duration),
                PreviousAudio = audio.PreviousAudio?.Name,
                NextAudio = audio.NextAudio?.Name
            })
            .ToList();
    }
}