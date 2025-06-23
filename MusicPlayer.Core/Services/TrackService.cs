using MusicPlayer.Core.Entities.Business;
using MusicPlayer.Core.Entities.General;
using MusicPlayer.Core.Interfaces.IRepositories;
using MusicPlayer.Core.Interfaces.IServices;

namespace MusicPlayer.Core.Services;

public class TrackService : ITrackService
{
    private readonly ITrackRepository _trackRepository;

    public TrackService(ITrackRepository trackRepository)
    {
        _trackRepository = trackRepository;
    }

    public async Task<TrackViewModel> CreateTrackAsync(TrackCreateViewModel model)
    {
        var album = new Album
        {
            Name = model.Album,
            AlbumType = Enums.AlbumType.Single, 
            Popularity = model.Popularity,
            ReleaseDate = DateTime.UtcNow,
            TotalTracks = 1
        };

        var mappedTrack = new Track
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Duration = model.Duration,
            Popularity = model.Popularity,
            Album = album,
            CreatedAt = DateTime.UtcNow,
            Genre = model.Genre,
        };
        mappedTrack.Album = album;

        await _trackRepository.CreateAsync(mappedTrack);

        return new TrackViewModel
        {
            Id = mappedTrack.Id.ToString(),
            Name = mappedTrack.Name,
            Duration = mappedTrack.Duration,
            Popularity = mappedTrack.Popularity,
            Genre = mappedTrack.Genre,
            CoverImageUrl = mappedTrack.CoverImageUrl ?? "",
            CreatedAt = mappedTrack.CreatedAt,
            Album = mappedTrack.Album.Name
        };
    }

    public async Task DeleteTrackAsync(Guid id)
    {
        var track = await _trackRepository.GetByIdAsync(id);
        if (track == null)
        {
            throw new KeyNotFoundException($"Track with ID {id} not found.");
        }

        await _trackRepository.DeleteAsync(track);
    }

    public async Task UpdateTrackAsync(Guid id, TrackUpdateViewModel model)
    {
        var existingTrack = await _trackRepository.GetByIdAsync(id);

        if (existingTrack == null)
        {
            throw new KeyNotFoundException($"Track with ID {id} not found.");
        }

        existingTrack.Name = model.Name;
        existingTrack.Duration = model.Duration;
        existingTrack.Popularity = model.Popularity;
        existingTrack.Genre = model.Genre;
        existingTrack.CoverImageUrl = model.CoverImageUrl ?? existingTrack.CoverImageUrl;
        existingTrack.UpdatedAt = DateTime.UtcNow;

        await _trackRepository.UpdateAsync(existingTrack);
    }

    public async Task<PaginationResponse<TrackViewModel>> GetAllTracksAsync(int page, int pageSize)
    {
        var tracks = await _trackRepository.GetAllAsync(page, pageSize);
        var items = tracks.Select(t => new TrackViewModel
        {
            Id = t.Id.ToString(),
            Name = t.Name,
            Duration = t.Duration,
            Popularity = t.Popularity,
            Genre = t.Genre,
            CoverImageUrl = t.CoverImageUrl ?? "",
            CreatedAt = t.CreatedAt,
            Album = t.Album?.Name ?? "Unknown Album"
        });

        return new PaginationResponse<TrackViewModel>(
            page, pageSize, tracks.LongCount(), [.. items]);
    }

    public async Task<TrackViewModel> GetTrackByIdAsync(Guid trackId)
    {
        var track = await _trackRepository.GetByIdAsync(trackId);

        if (track == null)
        {
            throw new KeyNotFoundException($"Track with ID {trackId} not found.");
        }
        
        return new TrackViewModel
        {
            Id = track.Id.ToString(),
            Name = track.Name,
            Duration = track.Duration,
            Popularity = track.Popularity,
            Genre = track.Genre,
            CoverImageUrl = track.CoverImageUrl ?? "",
            CreatedAt = track.CreatedAt,
            Album = track.Album?.Name ?? "Unknown Album"
        };
    }
}
