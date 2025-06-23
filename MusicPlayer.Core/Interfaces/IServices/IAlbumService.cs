using MusicPlayer.Core.Entities.Business;
using MusicPlayer.Core.Entities.General;

namespace MusicPlayer.Core.Interfaces.IServices;

public interface IAlbumService
{
    Task<PaginationResponse<AlbumViewModel>> GetAllAsync(int page, int pageSize);
    Task<AlbumViewModel> GetAlbumByIdAsync(Guid albumId);
    Task<AlbumViewModel> CreateAlbumAsync(AlbumCreateViewModel model);
    Task UpdateAlbumAsync(Guid id, AlbumUpdateViewModel model);
    Task DeleteAlbumAsync(Guid id);
}
