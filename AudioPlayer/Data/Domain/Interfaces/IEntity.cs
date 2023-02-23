#nullable enable
namespace AudioPlayer.Data.Domain.Interfaces;

public interface IEntity<T>
{
    T Id { get; set; }
}