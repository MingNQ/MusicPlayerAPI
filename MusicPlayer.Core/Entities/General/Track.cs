namespace MusicPlayer.Core.Entities.General;

public class Track
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Popularity { get; set; }
    public int TrackNumber { get; set; }
    public int Duration { get; set; }
    public string Genre { get; set; } = default!;
    public string CoverImageUrl { get; set; } = default!;
    public ICollection<PlaylistTrack> PlaylistTracks { get; set; } = [];
    public ICollection<Artist> Artist { get; set; } = [];
    public Album Album { get; set; } = default!;
}
