namespace MusicPlayer.Core.Entities.Business;

public class ArtistViewModel
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public int Popularity { get; set; }
    public bool IsActive { get; set; }
    public List<string> Followers { get; set; } = [];
    public List<string> TopTracks { get; set; } = [];
    public List<string> Albums { get; set; } = [];
    public List<string> Tracks { get; set; } = [];
}

public class ArtistCreateViewModel
{

}

public class ArtistUpdateViewModel
{

}
