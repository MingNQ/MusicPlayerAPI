using MusicPlayer.Core.Entities.Business;
using MusicPlayer.Core.Entities.General;
using MusicPlayer.Core.Interfaces.IRepositories;
using MusicPlayer.Core.Interfaces.IServices;

namespace MusicPlayer.Core.Services;

public class ArtistService : IArtistService
{
    private readonly IArtistRepository _artistRepository;

    public ArtistService(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<ArtistViewModel> GetArtistByIdAsync(Guid artistId)
    {
        if (artistId == Guid.Empty)
        {
            throw new ArgumentException("Artist ID cannot be empty.", nameof(artistId));
        }
        var result = await _artistRepository.GetByIdAsync(artistId);
        
        return new ArtistViewModel
        {
            Id = result.Id.ToString(),
            Name = result.Name,
            ImageUrl = result.ImageUrl ?? "",
            Popularity = result.Popularity,
            IsActive = result.IsActive,
            CreatedAt = result.CreatedAt,
            UpdatedAt = result.UpdatedAt,
            Followers = [.. result.Followers.Select(f => f.TargetId.ToString())], // Assuming Followers is a collection of Follow entities with UserId property
            Albums = [.. result.Albums.Select(a => a.Id.ToString())], // Assuming Albums is a collection of Album entities with Id property
            Tracks = [.. result.Tracks.Select(t => t.Id.ToString())], // Assuming Tracks is a collection of Track entities with Id property
            TopTracks = [.. result.Tracks.OrderByDescending(t => t.Popularity).Take(5).Select(t => t.Id.ToString())] // Assuming Tracks has a Popularity property
        };
    }

    public async Task<PaginationResponse<ArtistViewModel>> GetArtistsAsync(int page = 0, int pageSize = 0)
    {
        var artists = await _artistRepository.GetAllAsync(page, pageSize);
        var items = artists.Select(a => new ArtistViewModel
        {
            Id = a.Id.ToString(),
            Name = a.Name,
            ImageUrl = a.ImageUrl ?? "",
            Popularity = a.Popularity,
            IsActive = a.IsActive,
            CreatedAt = a.CreatedAt,
            UpdatedAt = a.UpdatedAt,
            Followers = [.. a.Followers.Select(f => f.TargetId.ToString())],
            Albums = [.. a.Albums.Select(al => al.Id.ToString())],
            Tracks = [.. a.Tracks.Select(t => t.Id.ToString())],
            TopTracks = [.. a.Tracks.OrderByDescending(t => t.Popularity).Take(5).Select(t => t.Id.ToString())]
        });

        return new PaginationResponse<ArtistViewModel>(
            page, pageSize, artists.LongCount(),
            items: items);
    }

    public async Task<ArtistViewModel> CreateArtistAsync(ArtistCreateViewModel model)
    {
        var mappedArtist = new Artist
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            ImageUrl = model.ImageUrl ?? "",
            Popularity = model.Popularity,
            IsActive = true,
            Followers = [],
            Albums = [],
            Tracks = [],
            CreatedAt = DateTime.UtcNow
        };

        var result = await _artistRepository.CreateAsync(mappedArtist);
        var mappedResult = new ArtistViewModel
        {
            Id = result.Id.ToString(),
            Name = result.Name,
            ImageUrl = result.ImageUrl ?? "",
            Popularity = result.Popularity,
            IsActive = result.IsActive,
            CreatedAt = result.CreatedAt,
            UpdatedAt = result.UpdatedAt
        };

        return mappedResult;
    }

    public async Task DeleteArtistAsync(Guid artistId)
    {
        if (artistId == Guid.Empty)
        {
            throw new ArgumentException("Artist ID cannot be empty.", nameof(artistId));
        }
        var model = await _artistRepository.GetByIdAsync(artistId);
        await _artistRepository.DeleteAsync(model);
    }

    public async Task UpdateArtistAsync(Guid modelId, ArtistUpdateViewModel model)
    {
        var existingArtist = await _artistRepository.GetByIdAsync(modelId);
        
        var mappedArtist = new Artist
        {
            Id = existingArtist.Id,
            Name = model.Name,
            ImageUrl = model.ImageUrl ?? existingArtist.ImageUrl,
            Popularity = model.Popularity,
            Followers = existingArtist.Followers, // Assuming followers are not updated here
            Albums = existingArtist.Albums, // Assuming albums are not updated here
            Tracks = existingArtist.Tracks, // Assuming tracks are not updated here
            CreatedAt = existingArtist.CreatedAt,
            UpdatedAt = DateTime.UtcNow
        };

        await _artistRepository.UpdateAsync(mappedArtist);
    }

    public async Task<IEnumerable<Track>> GetTopTracks(Guid artistId, int? top)
    {
        if (artistId == Guid.Empty)
        {
            throw new ArgumentException("Artist ID cannot be empty.", nameof(artistId));
        }
        
        var result = await _artistRepository.GetByIdAsync(artistId);
        var topValue = top ?? 5; // Default to 5 if top is null

        return [.. result.Tracks
            .OrderByDescending(t => t.Popularity)
            .Take(topValue)]; // Assuming Tracks is a collection of Track entities with Popularity property
    }
}
