#nullable enable
using System.Data.Entity;

namespace AudioPlayer.Models;

public sealed class ApplicationContext : DbContext
{
    public ApplicationContext()
        : base("DefaultConnection")
    {
            
    }
}