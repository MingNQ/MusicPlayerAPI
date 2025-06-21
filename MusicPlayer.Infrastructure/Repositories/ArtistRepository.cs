using MusicPlayer.Core.Entities.General;
using MusicPlayer.Core.Interfaces.IRepositories;
using MusicPlayer.Infrastructure.Data;

namespace MusicPlayer.Infrastructure.Repositories;

public class ArtistRepository: BaseRepository<Artist>, IArtistRepository
{
    public ArtistRepository(MusicPlayerDbContext dbContext) : base(dbContext)
    {
    }
}
