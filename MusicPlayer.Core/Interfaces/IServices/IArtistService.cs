using MusicPlayer.Core.Entities.Business;
using MusicPlayer.Core.Entities.General;

namespace MusicPlayer.Core.Interfaces.IServices;

public interface IArtistService
{
    Task<ArtistViewModel> GetArtistByIdAsync(Guid artistId);
    Task<PaginationResponse<ArtistViewModel>> GetArtistsAsync(int page = 0, int pageSize = 0);
    Task<ArtistViewModel> CreateArtistAsync(ArtistCreateViewModel model);
    Task UpdateArtistAsync(Guid modelId, ArtistUpdateViewModel model);
    Task DeleteArtistAsync(Guid artistId);
    Task<IEnumerable<Track>> GetTopTracks(Guid artistId, int? top);
    Task<PaginationResponse<AlbumViewModel>> GetAlbumByArtistIdAsync(Guid artistId);
}
