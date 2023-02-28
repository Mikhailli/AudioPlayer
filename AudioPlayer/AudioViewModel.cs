namespace AudioPlayer;

public class AudioViewModel
{
    public int Id { get; set; }
    public int NumberInPlayList { get; set; }
    public string Name { get; set; }

    public string NameWithExtension { get; set; }

    public string Path { get; set; }

    public string Duration { get; set; }

    public string? PreviousAudio { get; set; }
    
    public string? NextAudio { get; set; }
}