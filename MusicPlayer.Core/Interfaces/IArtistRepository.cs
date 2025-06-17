using MusicPlayer.Core.Entities.General;

namespace MusicPlayer.Core.Interfaces;

public interface IArtistRepository
{
    Task<Artist?> GetArtistByIdAsync(Guid artistId);
    Task<IEnumerable<Artist>> GetArtistsAsync(int page, int pageSize);
    Task CreateArtistAsync(Artist artist);
    Task UpdateArtistAsync(Artist artist);
    Task DeleteArtistAsync(Guid artistId);
}
