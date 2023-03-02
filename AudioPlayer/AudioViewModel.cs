namespace AudioPlayer;

public class AudioViewModel
{
    public int Id { get; set; }
    public int NumberInPlayList { get; set; }
    public string Name { get; set; } = null!;

    public string NameWithExtension { get; set; } = null!;

    public string Path { get; set; } = null!;

    public string Duration { get; set; } = null!;

    public string? PreviousAudio { get; set; }
    
    public string? NextAudio { get; set; }
}