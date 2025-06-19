using MusicPlayer.Core.Entities.General;

namespace MusicPlayer.Core.Interfaces.IRepositories;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<IEnumerable<User>> GetUsersAsync(int page, int pageSize);
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(Guid userId);
}
