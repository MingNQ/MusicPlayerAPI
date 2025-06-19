using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MusicPlayer.Infrastructure.Data;

public class MusicPlayerDbContextFactory : IDesignTimeDbContextFactory<MusicPlayerDbContext>
{
    public MusicPlayerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MusicPlayerDbContext>();
        optionsBuilder.UseSqlServer("Server=.;Database=MusicPlayerApiDb;Trusted_Connection=True;TrustServerCertificate=True");

        return new MusicPlayerDbContext(optionsBuilder.Options);
    }
}
