namespace MusicPlayer.Core.Entities.Business;

public class TrackViewModel
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Album { get; set; } = default!;
    public int Popularity { get; set; }
    public int TrackNumber { get; set; }
    public int Duration { get; set; }
    public string Genre { get; set; } = default!;
    public string? CoverImageUrl { get; set; }
    public List<string> Artist { get; set; } = [];
    public List<string> Playlists { get; set; } = [];
}

public class TrackCreateViewModel
{
}

public class TrackUpdateViewModel
{
}