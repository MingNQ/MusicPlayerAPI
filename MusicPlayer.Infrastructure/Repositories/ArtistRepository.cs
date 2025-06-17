using MusicPlayer.Core.Entities.General;
using MusicPlayer.Core.Interfaces;

namespace MusicPlayer.Infrastructure.Repositories;

public class ArtistRepository : IArtistRepository
{
    public Task CreateArtistAsync(Artist artist)
    {
        throw new NotImplementedException();
    }

    public Task DeleteArtistAsync(Guid artistId)
    {
        throw new NotImplementedException();
    }

    public Task<Artist?> GetArtistByIdAsync(Guid artistId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Artist>> GetArtistsAsync(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task UpdateArtistAsync(Artist artist)
    {
        throw new NotImplementedException();
    }
}
