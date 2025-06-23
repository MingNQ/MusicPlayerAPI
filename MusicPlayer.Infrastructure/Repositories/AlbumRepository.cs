using MusicPlayer.Core.Entities.General;
using MusicPlayer.Core.Interfaces.IRepositories;
using MusicPlayer.Infrastructure.Data;

namespace MusicPlayer.Infrastructure.Repositories;

public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
{
    public AlbumRepository(MusicPlayerDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Album> CreateAlbumAsync(Album model)
    {
        foreach (var track in model.Tracks)
        {
            var existingTrack = await _dbContext.Tracks.FindAsync(track.Id);
            if (existingTrack == null)
            {
                track.Id = Guid.NewGuid();
            }
            
            await _dbContext.Tracks.AddAsync(track);
        }

        foreach (var artist in model.Artist)
        {
            var existingArtist = await _dbContext.Artists.FindAsync(artist.Id);
            if (existingArtist == null)
            {
                artist.Id = Guid.NewGuid();
            }

            await _dbContext.Artists.AddAsync(artist);
        }

        await _dbContext.Albums.AddAsync(model);

        return model;
    }
}
