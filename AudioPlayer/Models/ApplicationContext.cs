#nullable enable
using Microsoft.EntityFrameworkCore;

namespace AudioPlayer.Models;

public sealed class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
            
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=audioplayer;Trusted_Connection=True;");
        
}