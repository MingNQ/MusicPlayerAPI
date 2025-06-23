using MusicPlayer.Core.Enums;

namespace MusicPlayer.Core.Entities.Business;

public class AlbumViewModel
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? CoverImageUrl { get; set; }
    public int Popularity { get; set; }
    public DateTime ReleaseDate { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public AlbumType AlbumType { get; set; } = default!;
    public int TotalTracks { get; set; }
    public List<TrackViewModel> Tracks { get; set; } = [];
    public List<ArtistViewModel> Artist { get; set; } = [];
}

public class AlbumCreateViewModel
{
    public string Name { get; set; } = default!;
    public string? CoverImageUrl { get; set; }
    public int Popularity { get; set; }
    public DateTime ReleaseDate { get; set; }
    public AlbumType AlbumType { get; set; } = default!;
    public int TotalTracks { get; set; }
    public List<TrackViewModel> Tracks { get; set; } = [];
    public List<ArtistViewModel> Artist { get; set; } = [];
}

public class AlbumUpdateViewModel
{
    public string Name { get; set; } = default!;
    public string? CoverImageUrl { get; set; }
    public int Popularity { get; set; }
    public DateTime ReleaseDate { get; set; }
    public AlbumType AlbumType { get; set; } = default!;
    public int TotalTracks { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<TrackViewModel> Tracks { get; set; } = [];
    public List<ArtistViewModel> Artist { get; set; } = [];
}