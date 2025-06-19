using MusicPlayer.Core.Entities.General;
using MusicPlayer.Core.Interfaces.IRepositories;

namespace MusicPlayer.Infrastructure.Repositories;

public class TrackRepository : ITrackRepository
{
    public Task CreateTrackAsync(Track track)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTrackAsync(Guid trackId)
    {
        throw new NotImplementedException();
    }

    public Task<Track?> GetTrackByIdAsync(Guid trackId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Track>> GetTracksAsync(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTrackAsync(Track track)
    {
        throw new NotImplementedException();
    }
}
