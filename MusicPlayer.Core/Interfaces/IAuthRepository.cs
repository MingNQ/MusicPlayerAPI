using MusicPlayer.Core.Entities.Business;

namespace MusicPlayer.Core.Interfaces;

public interface IAuthRepository
{
    Task<ResponseViewModel<UserViewModel>> LoginAsync(string userName, string password);
    Task LogoutAsync();
}
