#nullable enable
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AudioPlayer.Data.Domain.Interfaces;

namespace AudioPlayer.Models;

public class Audio : Entity<int>
{
    public string Name { get; set; } = null!;
    public string Path { get; set; } = null!;
    public int Duration { get; set; }
    public Audio? PreviousAudio { get; set; }
    public Audio? NextAudio { get; set; }
    [JsonIgnore]
    [InverseProperty("PreviousAudio")]
    public ICollection<Audio?>? NextForPreviousAudio { get; set; }
    [InverseProperty("NextAudio")]
    [JsonIgnore]
    public ICollection<Audio?>? PreviousForNextAudio { get; set; }
}