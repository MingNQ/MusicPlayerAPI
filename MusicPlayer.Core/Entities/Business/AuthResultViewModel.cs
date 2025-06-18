namespace MusicPlayer.Core.Entities.Business;

public class AuthResultViewModel
{
    public string AccessToken { get; set; } = string.Empty;
    public bool Success { get; set; }
    public List<string>? Errors { get; set; }
}
