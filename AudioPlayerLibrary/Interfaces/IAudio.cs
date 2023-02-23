#nullable enable

namespace AudioPlayerLibrary.Interfaces;

public interface IAudio
{
    public bool IsPaused { get; set; }
    public int? HowMuchSongPlaying { get; set; }
    public int Duration { get; set; }

    public IAudio? Previous { get; set; }

    public IAudio? Next { get; set; }
}