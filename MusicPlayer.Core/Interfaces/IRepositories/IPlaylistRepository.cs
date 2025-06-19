using MusicPlayer.Core.Entities.General;

namespace MusicPlayer.Core.Interfaces.IRepositories;

public interface IPlaylistRepository
{
    Task<Playlist?> GetPlaylistByIdAsync(Guid playlistId);
    Task<IEnumerable<Playlist>> GetPlaylistsAsync(int page, int pageSize);
    Task CreatePlaylistAsync(Playlist playlist);
    Task UpdatePlaylistAsync(Playlist playlist);
    Task DeletePlaylistAsync(Guid playlistId);
}
