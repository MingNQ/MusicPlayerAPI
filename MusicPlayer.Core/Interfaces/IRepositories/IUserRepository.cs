using Microsoft.AspNetCore.Identity;
using MusicPlayer.Core.Entities.Business;
using MusicPlayer.Core.Entities.General;

namespace MusicPlayer.Core.Interfaces.IRepositories;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<IEnumerable<User>> GetUsersAsync(int page, int pageSize);
    Task<IdentityResult> CreateUserAsync(UserCreateViewModel model);
    Task<IdentityResult> UpdateUserAsync(UserUpdateViewModel model);
    Task<IdentityResult> DeleteUserAsync(Guid userId);
}
