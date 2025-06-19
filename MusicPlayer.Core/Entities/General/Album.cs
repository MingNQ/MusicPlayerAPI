using MusicPlayer.Core.Enums;

namespace MusicPlayer.Core.Entities.General;

public class Album
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string CoverImageUrl { get; set; } = default!;
    public int Popularity { get; set; }
    public DateTime ReleaseDate { get; set; }
    public AlbumType AlbumType { get; set; } = default!;
    public int TotalTracks { get; set; }
    public ICollection<Track> Tracks { get; set; } = [];
    public ICollection<Artist> Artist { get; set; } = [];
}
