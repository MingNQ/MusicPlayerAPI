using Microsoft.AspNetCore.Identity;

namespace MusicPlayer.Core.Entities.General;

public class User : IdentityUser<Guid>
{
    public string? ProfileImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Playlist> Playlists { get; set; } = default!;
    public ICollection<Follow> Followers { get; set; } = default!;
    public ICollection<Follow> Following { get; set; } = default!;
}
