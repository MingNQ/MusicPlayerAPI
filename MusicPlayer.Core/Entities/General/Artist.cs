namespace MusicPlayer.Core.Entities.General;

public class Artist
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? ImageUrl { get; set; } = default!;
    public int Popularity { get; set; }
    public bool IsActive { get; set; }
    public ICollection<Follow> Followers { get; set; } = [];
    public ICollection<Album> Albums { get; set; } = [];
    public ICollection<Track> Tracks { get; set; } = [];
}
