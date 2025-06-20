using MusicPlayer.Core.Entities.Business;

namespace MusicPlayer.Core.Interfaces.IServices;

public interface IUserService
{
    Task<UserViewModel> GetUserByIdAsync(Guid userId);
    Task<PaginationResponse<UserViewModel>> GetUsersAsync(int page = 0, int pageSize = 0);
    Task<ResponseViewModel> CreateUserAsync(UserCreateViewModel model);
    Task<ResponseViewModel> UpdateUserAsync(UserUpdateViewModel model);
    Task DeleteUserAsync(Guid userId);
}
