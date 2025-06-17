using MusicPlayer.Core.Entities.General;
using MusicPlayer.Core.Interfaces;

namespace MusicPlayer.Infrastructure.Repositories;

public class AlbumRepository : IAlbumRepository
{
    public Task CreateAlbumAsync(Album album)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAlbumAsync(Guid albumId)
    {
        throw new NotImplementedException();
    }

    public Task<Album?> GetAlbumByIdAsync(Guid albumId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Album>> GetAlbumsAsync(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAlbumAsync(Album album)
    {
        throw new NotImplementedException();
    }
}
