using MusicPlayer.Core.Entities.Business;
using MusicPlayer.Core.Entities.General;
using MusicPlayer.Core.Interfaces.IRepositories;
using MusicPlayer.Core.Interfaces.IServices;

namespace MusicPlayer.Core.Services;

public class AlbumService : IAlbumService
{
    private readonly IAlbumRepository _albumRepository;

    public AlbumService(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<AlbumViewModel> CreateAlbumAsync(AlbumCreateViewModel model)
    {
        var mappedAlbum = new Album
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            CoverImageUrl = model.CoverImageUrl,
            Popularity = model.Popularity,
            ReleaseDate = model.ReleaseDate,
            AlbumType = model.AlbumType,
            TotalTracks = model.TotalTracks,
        };

        foreach (var trackVM in model.Tracks)
        {
            var track = new Track
            {
                Id = trackVM.Id != null ? Guid.Parse(trackVM.Id) : Guid.NewGuid(),
                Name = trackVM.Name,
                Duration = trackVM.Duration,
                Genre = trackVM.Genre,
                CoverImageUrl = trackVM.CoverImageUrl,
                Popularity = trackVM.Popularity,
                CreatedAt = DateTime.UtcNow,
                Album = mappedAlbum
            };

            mappedAlbum.Tracks.Add(track);
        }
        mappedAlbum.TotalTracks = mappedAlbum.Tracks.Count;


        foreach (var artistVM in model.Artist)
        {
            var artist = new Artist
            {
                Id = artistVM.Id != null ? Guid.Parse(artistVM.Id) : Guid.NewGuid(),
                Name = artistVM.Name,
                ImageUrl = artistVM.ImageUrl,
                Popularity = artistVM.Popularity,
                IsActive = true,
                CreatedAt = artistVM.CreatedAt,
                Tracks = mappedAlbum.Tracks
            };

            artist.Albums.Add(mappedAlbum);

            mappedAlbum.Artist.Add(artist);
        }

        await _albumRepository.CreateAsync(mappedAlbum);

        return new AlbumViewModel
        {
            Id = mappedAlbum.Id.ToString(),
            Name = model.Name,
            CoverImageUrl = mappedAlbum.CoverImageUrl,
            Popularity = mappedAlbum.Popularity,
            ReleaseDate = mappedAlbum.ReleaseDate,
            AlbumType = mappedAlbum.AlbumType,
            TotalTracks = mappedAlbum.TotalTracks,
            Tracks = model.Tracks,
            Artist = model.Artist,
        };
    }

    public async Task UpdateAlbumAsync(Guid id, AlbumUpdateViewModel model)
    {
        var existingAlbum = await _albumRepository.GetByIdAsync(id);

        if (existingAlbum == null)
        {
            throw new KeyNotFoundException($"Album with ID {id} not found.");
        }

        existingAlbum.Name = model.Name;
        existingAlbum.CoverImageUrl = model.CoverImageUrl;
        existingAlbum.Popularity = model.Popularity;
        existingAlbum.ReleaseDate = model.ReleaseDate;
        existingAlbum.AlbumType = model.AlbumType;
        existingAlbum.TotalTracks = model.TotalTracks;
        existingAlbum.UpdatedAt = DateTime.UtcNow;

        await _albumRepository.UpdateAsync(existingAlbum);
    }

    public async Task DeleteAlbumAsync(Guid id)
    {
        var existingAlbum = await _albumRepository.GetByIdAsync(id);
        if (existingAlbum == null)
        {
            throw new KeyNotFoundException($"Album with ID {id} not found.");
        }
        await _albumRepository.DeleteAsync(existingAlbum);
    }

    public async Task<AlbumViewModel> GetAlbumByIdAsync(Guid albumId)
    {
        var existingAlbum = await _albumRepository.GetByIdAsync(albumId);
        if (existingAlbum == null)
        {
            throw new KeyNotFoundException($"Album with ID {albumId} not found.");
        }

        var albumViewModel = new AlbumViewModel
        {
            Id = existingAlbum.Id.ToString(),
            Name = existingAlbum.Name,
            CoverImageUrl = existingAlbum.CoverImageUrl,
            Popularity = existingAlbum.Popularity,
            ReleaseDate = existingAlbum.ReleaseDate,
            AlbumType = existingAlbum.AlbumType,
        };

        foreach (var trackVM in existingAlbum.Tracks)
        {
            var track = new TrackViewModel
            {
                Id = trackVM.Id.ToString(),
                Name = trackVM.Name,
                Duration = trackVM.Duration,
                Genre = trackVM.Genre,
                CoverImageUrl = trackVM.CoverImageUrl ?? "",
                Popularity = trackVM.Popularity,
            };

            albumViewModel.Tracks.Add(track);
        }

        foreach (var artistVM in existingAlbum.Artist)
        {
            var artist = new ArtistViewModel
            {
                Id = artistVM.Id.ToString(),
                Name = artistVM.Name,
                ImageUrl = artistVM.ImageUrl ?? "",
                Popularity = artistVM.Popularity,
                IsActive = true,
                CreatedAt = artistVM.CreatedAt
            };

            albumViewModel.Artist.Add(artist);
        }

        return albumViewModel;
    }

    public async Task<PaginationResponse<AlbumViewModel>> GetAllAsync(int page, int pageSize)
    {
        var albums = await _albumRepository.GetAllAsync(page, pageSize);
        
        var items = albums.Select(a => new AlbumViewModel
        {
            Id = a.Id.ToString(),
            Name = a.Name,
            CoverImageUrl = a.CoverImageUrl,
            Popularity = a.Popularity,
            ReleaseDate = a.ReleaseDate,
            AlbumType = a.AlbumType,
        });

        return new PaginationResponse<AlbumViewModel>(
            page, pageSize, albums.LongCount(), items);
    }
}
