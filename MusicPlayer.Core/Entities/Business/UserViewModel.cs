namespace MusicPlayer.Core.Entities.Business;

public class UserViewModel
{
    public string Id { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string? ProfileImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<string> Playlists { get; set; } = [];
    public List<string> Followers { get; set; } = [];
    public List<string> Following { get; set; } = [];
}

public class UserCreateViewModel
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string Password { get; set; } = default!;
    public string? ProfileImageUrl { get; set; }
    public DateTime? CreatedAt { get; set; }
}

public class UserUpdateViewModel
{
    public string Id { get; set; } = default!;
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfileImageUrl { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
