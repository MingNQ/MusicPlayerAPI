using MusicPlayer.Core.Entities.General;

namespace MusicPlayer.Core.Interfaces;

public interface ITrackRepository
{
    Task<Track?> GetTrackByIdAsync(Guid trackId);
    Task<IEnumerable<Track>> GetTracksAsync(int page, int pageSize);
    Task CreateTrackAsync(Track track);
    Task UpdateTrackAsync(Track track);
    Task DeleteTrackAsync(Guid trackId);
}
