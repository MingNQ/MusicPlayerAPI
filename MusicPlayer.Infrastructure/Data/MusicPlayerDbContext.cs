using Microsoft.EntityFrameworkCore;

namespace MusicPlayer.Infrastructure.Data;

public class MusicPlayerDbContext : DbContext
{
    public MusicPlayerDbContext(DbContextOptions<MusicPlayerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
