namespace MusicPlayer.Core.Entities.General;

public class Playlist
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? CoverImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool Public { get; set; }
    public ICollection<PlaylistTrack> PlaylistTracks { get; set; } = [];
    public User Owner { get; set; } = default!;
}
