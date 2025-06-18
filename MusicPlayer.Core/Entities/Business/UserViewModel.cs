namespace MusicPlayer.Core.Entities.Business;

public class UserViewModel
{
    public Guid Id { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string Password { get; set; } = default!;
}

    public class UserCreateViewModel
{
}

public class UserUpdateViewModel
{
}
