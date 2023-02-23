#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using AudioPlayerLibrary;
using AudioPlayerLibrary.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AudioPlayerTests;

[TestClass]
public class AudioPlayerTests
{
    
    [TestMethod]
    public void Test1()
    {
        var audioPlayer = InitializeAudioPlayer();
        audioPlayer.Play(audioPlayer._audios.First());
        Assert.IsTrue(audioPlayer._audios.Count == 4);
    }

    private AudioPlayer InitializeAudioPlayer()
    {
        var audio1 = new Audio(10, null, null);
        var audio2 = new Audio(20, audio1, null);
        var audio3 = new Audio(30, audio2, null);
        var audio4 = new Audio(40, audio3, null);

        var collection = new List<IAudio>();
        var audioPlayer = new AudioPlayer(collection);
        audioPlayer.AddSong(audio1);
        audioPlayer.AddSong(audio2);
        audioPlayer.AddSong(audio3);
        audioPlayer.AddSong(audio4);

        return audioPlayer;
    }
}