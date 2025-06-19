namespace MusicPlayer.Core.Entities.Business;

public class AlbumViewModel
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string CoverImageUrl { get; set; } = default!;
    public int Popularity { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string AlbumType { get; set; } = default!;
    public int TotalTracks { get; set; }
    public List<string> Tracks { get; set; } = [];
    public List<string> Artist { get; set; } = [];
}

public class AlbumCreateViewModel
{
}

public class AlbumUpdateViewModel
{
}