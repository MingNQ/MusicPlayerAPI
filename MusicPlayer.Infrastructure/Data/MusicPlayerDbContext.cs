using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MusicPlayer.Infrastructure.Data;

public class MusicPlayerDbContext : IdentityDbContext
{
    public MusicPlayerDbContext(DbContextOptions<MusicPlayerDbContext> options) : base(options)
    {
    }
}
