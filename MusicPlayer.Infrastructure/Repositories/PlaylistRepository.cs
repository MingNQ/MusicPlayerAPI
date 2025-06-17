using MusicPlayer.Core.Entities.General;
using MusicPlayer.Core.Interfaces;

namespace MusicPlayer.Infrastructure.Repositories;

public class PlaylistRepository : IPlaylistRepository
{
    public Task CreatePlaylistAsync(Playlist playlist)
    {
        throw new NotImplementedException();
    }

    public Task DeletePlaylistAsync(Guid playlistId)
    {
        throw new NotImplementedException();
    }

    public Task<Playlist?> GetPlaylistByIdAsync(Guid playlistId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Playlist>> GetPlaylistsAsync(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePlaylistAsync(Playlist playlist)
    {
        throw new NotImplementedException();
    }
}
