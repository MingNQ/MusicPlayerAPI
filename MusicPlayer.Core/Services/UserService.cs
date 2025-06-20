using MusicPlayer.Core.Entities.Business;
using MusicPlayer.Core.Interfaces.IRepositories;
using MusicPlayer.Core.Interfaces.IServices;

namespace MusicPlayer.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResponseViewModel> CreateUserAsync(UserCreateViewModel model)
    {
        if (model == null)
        {
            return new ResponseViewModel
            {
                Success = false,
                Message = "User creation failed",
                Error = new ErrorViewModel
                {
                    Code = "USER_CREATION_ERROR",
                    Message = "User model cannot be null."
                }
            };
        }

        var result = await _userRepository.CreateUserAsync(model);

        if (result.Succeeded)
        {
            return new ResponseViewModel
            {
                Success = true,
                Message = "User created successfully."
            };
        }
        else
        {
            return new ResponseViewModel
            {
                Success = false,
                Message = "User creation failed",
                Error = new ErrorViewModel
                {
                    Code = "USER_CREATION_ERROR",
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                }
            };
        }
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var user = _userRepository.DeleteUserAsync(userId);

        await _userRepository.DeleteUserAsync(userId);
    }

    public async Task<UserViewModel> GetUserByIdAsync(Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId) ?? throw new KeyNotFoundException($"User with ID '{userId}' not found.");

        return new UserViewModel
        {
            Id = user.Id.ToString(),
            Username = user.UserName!,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber,
            ProfileImageUrl = user.ProfileImageUrl,
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<PaginationResponse<UserViewModel>> GetUsersAsync(int page = 0, int pageSize = 0)
    {
        var users = await _userRepository.GetUsersAsync(page, pageSize);
        var userViewModels = users.Select(user => new UserViewModel
        {
            Id = user.Id.ToString(),
            Username = user.UserName!,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber,
            ProfileImageUrl = user.ProfileImageUrl,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt ?? DateTime.MinValue,
            Playlists = user.Playlists != null ? [.. user.Playlists.Select(p => p.Id.ToString())] : [],
            Followers = user.Followers != null ? [.. user.Followers.Select(f => f.FollowerId.ToString())] : [],
            Following = user.Following != null ? [.. user.Following.Select(f => f.TargetId.ToString())] : []
        });

        return new PaginationResponse<UserViewModel>(page, pageSize, users.LongCount(), userViewModels);
    }

    public Task<ResponseViewModel> UpdateUserAsync(UserUpdateViewModel model)
    {
        var result = _userRepository.UpdateUserAsync(model);
        if (result.Result.Succeeded)
        {
            return Task.FromResult(new ResponseViewModel
            {
                Success = true,
                Message = "User updated successfully."
            });
        }
        else
        {
            return Task.FromResult(new ResponseViewModel
            {
                Success = false,
                Message = "User update failed",
                Error = new ErrorViewModel
                {
                    Code = "USER_UPDATE_ERROR",
                    Message = string.Join(", ", result.Result.Errors.Select(e => e.Description))
                }
            });
        }
    }
}
