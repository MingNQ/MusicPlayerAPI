using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.Core.Entities.Business;
using MusicPlayer.Core.Entities.General;
using MusicPlayer.Core.Interfaces.IRepositories;

namespace MusicPlayer.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> CreateUserAsync(UserCreateViewModel model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "User cannot be null.");
        }
        
        var existingUser = await _userManager.FindByNameAsync(model.Username!);

        if (existingUser != null)
        {
            throw new InvalidOperationException($"User with username '{model.Username}' already exists.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = model.Username,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            ProfileImageUrl = model.ProfileImageUrl,
            CreatedAt = model.CreatedAt ?? DateTime.UtcNow
        };

        return await _userManager.CreateAsync(user, model.Password);
    }

    public async Task<IdentityResult> DeleteUserAsync(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User ID cannot be empty."} );
        }

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = $"User with ID '{userId}' does not exist." });
        }

        return await _userManager.DeleteAsync(user);
    }

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));
        }

        return await _userManager.FindByIdAsync(userId.ToString()) ?? throw new ArgumentException($"User with ID '{userId}' does not exist.");
    }

    public async Task<IEnumerable<User>> GetUsersAsync(int page, int pageSize)
    {
        var result = await _userManager.Users
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return result;
    }

    public async Task<IdentityResult> UpdateUserAsync(UserUpdateViewModel model)
    {
        if (model == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        var user = await _userManager.FindByIdAsync(model.Id);

        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = $"User with ID '{model.Id}' does not exist."} );
        }

        user.UserName = model.Username;
        user.Email = model.Email;
        user.PhoneNumber = model.PhoneNumber;
        user.ProfileImageUrl = model.ProfileImageUrl;
        user.UpdatedAt = model.UpdatedAt ?? DateTime.UtcNow;

        return await _userManager.UpdateAsync(user);
    }
}
