﻿#nullable enable
using System.Data.Entity;

namespace AudioPlayer.Models;

public sealed class ApplicationContext : DbContext
{
    public DbSet<Audio> Audio { get; set; } = null!;
    
    public ApplicationContext()
        : base("Server=localhost\\SQLEXPRESS01;Database=CloudioPlayer;Trusted_Connection=True;")
    {
        
    }
}