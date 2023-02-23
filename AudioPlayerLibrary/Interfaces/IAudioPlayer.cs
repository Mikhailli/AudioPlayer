#nullable enable
using System.Collections.Generic;
using System.Linq;

namespace AudioPlayerLibrary.Interfaces;

public abstract class Player
{
    public readonly List<IAudio> _audios;
    public IAudio? _currentAudio;

    public abstract void Play(IAudio audio);

    public abstract void Pause(IAudio audio);

    public void Next()
    {
        if (_currentAudio is null || _currentAudio.Next is null)
        {
            return;
        }
        
        Pause(_currentAudio);
        Play(_currentAudio.Next);
    }

    public void Prev()
    {
        if (_currentAudio is null || _currentAudio.Previous is null)
        {
            return;
        }
        
        Pause(_currentAudio);
        Play(_currentAudio.Previous);
    }

    protected Player(List<IAudio> audios)
    {
        _audios = audios;
        _currentAudio = _audios.FirstOrDefault();
    }
    
    public void AddSong(IAudio song)
    {
        if (_audios.Any())
        {
            var previousAudio = _audios.Last();
            _audios.Add(song);
            song.Previous = previousAudio;
            previousAudio.Next = song;
        }

        else
        {
            _audios.Add(song);
        }
    }
}