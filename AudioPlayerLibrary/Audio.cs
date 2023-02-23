#nullable enable
using AudioPlayerLibrary.Interfaces;

namespace AudioPlayerLibrary
{
    public class Audio : IAudio
    {
        public bool IsPaused { get; set; }
        public int? HowMuchSongPlaying { get; set; }
        public int Duration { get; set; }
        public IAudio? Previous { get; set; }
        public IAudio? Next { get; set; }

        public Audio(int duration, IAudio? previous, IAudio? next)
        {
            Duration = duration;
            Previous = previous;
            Next = next;
            HowMuchSongPlaying = null;
            IsPaused = false;
        }
    }
}