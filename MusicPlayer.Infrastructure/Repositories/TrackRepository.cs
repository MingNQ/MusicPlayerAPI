using MusicPlayer.Core.Entities.General;
using MusicPlayer.Core.Interfaces.IRepositories;
using MusicPlayer.Infrastructure.Data;

namespace MusicPlayer.Infrastructure.Repositories;

public class TrackRepository : BaseRepository<Track> ,ITrackRepository
{
    public TrackRepository(MusicPlayerDbContext dbContext) : base(dbContext)
    {
    }
}
