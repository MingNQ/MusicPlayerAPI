using MusicPlayer.Core.Entities.Business;

namespace MusicPlayer.Core.Interfaces.IRepositories;

public interface IAuthRepository
{
    Task<ResponseViewModel<UserViewModel>> LoginAsync(string userName, string password);
    Task LogoutAsync();
    Task<ResponseViewModel<UserViewModel>> RegisterAsync(string userName, string email, string? phoneNumber, string password);
}
