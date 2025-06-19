namespace MusicPlayer.Core.Entities.Business;

public class PlaylistViewModel
{
    public string Id { get; set; } = string.Empty;
    public string OwnerId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CoverImageUrl { get; set; } = string.Empty;
    public List<string> TrackIds { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool Public { get; set; }
}

public class PlaylistCreateViewModel
{
}

public class PlaylistUpdateViewModel
{
}