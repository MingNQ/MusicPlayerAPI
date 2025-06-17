using MusicPlayer.Core.Entities.Business;

namespace MusicPlayer.Core.Interfaces.IServices;

public interface IAuthService
{
    Task<ResponseViewModel<UserViewModel>> LoginAsync(string userName, string password);
    Task LogoutAsync();
}
