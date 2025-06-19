using MusicPlayer.Core.Entities.General;

namespace MusicPlayer.Core.Interfaces.IRepositories;

public interface IAlbumRepository
{
    Task<Album?> GetAlbumByIdAsync(Guid albumId);
    Task<IEnumerable<Album>> GetAlbumsAsync(int page, int pageSize);
    Task CreateAlbumAsync(Album album);
    Task UpdateAlbumAsync(Album album);
    Task DeleteAlbumAsync(Guid albumId);
}
