#nullable enable
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AudioPlayer.Data.Domain.Interfaces;
using AudioPlayerLibrary.Interfaces;

namespace AudioPlayer.Models;

public class Audio : Entity<int>, IAudio
{
    public string Name { get; set; } = null!;
    public string Path { get; set; } = null!;
   
    public int Duration { get; set; }
    public IAudio? Previous { get; set; }
    public IAudio? Next { get; set; }

    public virtual Audio? PreviousAudio { get; set; }
    public virtual Audio? NextAudio { get; set; }
    [JsonIgnore]
    [InverseProperty("PreviousAudio")]
    public virtual ICollection<Audio?>? NextForPreviousAudio { get; set; }
    [InverseProperty("NextAudio")]
    [JsonIgnore]
    public virtual ICollection<Audio?>? PreviousForNextAudio { get; set; }
}