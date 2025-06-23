using MusicPlayer.Core.Entities.General;

namespace MusicPlayer.Core.Interfaces.IRepositories;

public interface IAlbumRepository : IBaseRepository<Album>
{
    Task<Album> CreateAlbumAsync(Album model);
}
