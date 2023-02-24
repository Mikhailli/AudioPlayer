#nullable enable

namespace AudioPlayerLibrary.Interfaces;

public interface IAudio
{
    public int Duration { get; set; }

    public IAudio? Previous { get; set; }

    public IAudio? Next { get; set; }
}