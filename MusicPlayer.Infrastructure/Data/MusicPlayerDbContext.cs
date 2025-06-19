using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.Core.Entities.General;

namespace MusicPlayer.Infrastructure.Data;

public class MusicPlayerDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<PlaylistTrack> PlaylistTracks { get; set; }

    public MusicPlayerDbContext(DbContextOptions<MusicPlayerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        MusicPlayerDbContextConfiguration.Configure(builder);
    }
}
