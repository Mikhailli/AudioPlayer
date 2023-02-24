#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AudioPlayerLibrary.Interfaces;

namespace AudioPlayerLibrary;

public class AudioPlayer : Player
{
    public AudioPlayer(List<IAudio> audios) : base(audios)
    {
    }

    public override async void Play(IAudio audio)
    {
        //if (_currentAudio is not null)
        //{
        //    Pause(_currentAudio);
        //    _currentAudio.HowMuchSongPlaying = null;
        //}
        //_currentAudio = audio;
        //var howMuchSecondsLeft = audio.Duration - audio.HowMuchSongPlaying is null ? 0 : audio.HowMuchSongPlaying!.Value;
        //var task = new Task(() =>
        //    {
        //        var watch = new Stopwatch();
        //        watch.Start();
        //        while (watch.ElapsedMilliseconds < howMuchSecondsLeft * 1000)
        //        {
        //            if (_currentAudio.IsPaused)
        //            {
        //                _currentAudio.HowMuchSongPlaying = watch.ElapsedMilliseconds / 1000 as int?;
        //                watch.Stop();
        //                return;
        //            }
        //        }
        //        watch.Stop();
        //        Console.Write("Песня закончилась");
        //    }
        //);
        //task.Start();
        //if (_currentAudio.Next is null)
        //{
        //    Play(_audios.First());
        //}
        //else
        //{
        //    Play(_currentAudio.Next);
        //}
    }

    public override void Pause(IAudio audio)
    {
        if (_currentAudio is null)
        {
            return;
        }
        
        //_currentAudio.IsPaused = true;
    }
}