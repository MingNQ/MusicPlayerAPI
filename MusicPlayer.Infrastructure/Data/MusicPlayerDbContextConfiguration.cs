using Microsoft.EntityFrameworkCore;
using MusicPlayer.Core.Entities.General;

namespace MusicPlayer.Infrastructure.Data;

public class MusicPlayerDbContextConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Follow>()
            .HasOne(f => f.Follower)
            .WithMany(f => f.Following)
            .HasForeignKey(f => f.FollowerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PlaylistTrack>().HasKey(pt => new { pt.PlaylistId, pt.TrackId });
    }
}
