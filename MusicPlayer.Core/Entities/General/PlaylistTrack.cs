namespace MusicPlayer.Core.Entities.General;

public class PlaylistTrack
{
    public Guid PlaylistId { get; set; } = default!;
    public Guid TrackId { get; set; } = default!;
    public Playlist Playlist { get; set; } = default!;
    public Track Track { get; set; } = default!;
    public int Position { get; set; }
    public DateTime AddedAt { get; set; }
}
