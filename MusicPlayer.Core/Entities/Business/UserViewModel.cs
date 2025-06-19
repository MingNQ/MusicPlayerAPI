namespace MusicPlayer.Core.Entities.Business;

public class UserViewModel
{
    public string Id { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string ProfileImageUrl { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public List<string> Playlists { get; set; } = [];
    public List<string> Followers { get; set; } = [];
    public List<string> Following { get; set; } = [];
}

public class UserCreateViewModel
{
}

public class UserUpdateViewModel
{
}
