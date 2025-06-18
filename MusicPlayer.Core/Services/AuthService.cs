using MusicPlayer.Core.Entities.Business;
using MusicPlayer.Core.Interfaces;
using MusicPlayer.Core.Interfaces.IServices;

namespace MusicPlayer.Core.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<ResponseViewModel<UserViewModel>> LoginAsync(string userName, string password)
    {
        var result = await _authRepository.LoginAsync(userName, password);

        if (result.Success)
        {
            return new ResponseViewModel<UserViewModel>
            {
                Success = true,
                Data = result.Data,
                Message = "Login successful."
            };
        }
        else
        {
            // Handle login failure, e.g., logging or throwing an exception
            return new ResponseViewModel<UserViewModel>
            {
                Success = false,
                Message = "Login failed. Please check your credentials.",
                Error = new ErrorViewModel
                {
                    Code = "LOGIN_ERROR",
                    Message = result.Error?.Message ?? "An error occurred during login."
                }
            };
        }
    }

    public async Task LogoutAsync()
    {
        await _authRepository.LogoutAsync();
    }

    public async Task<ResponseViewModel<UserViewModel>> RegisterAsync(string userName, string email, string? phoneNumber, string password)
    {
        var result = await _authRepository.RegisterAsync(userName, email, phoneNumber, password);

        if (result.Success)
        {
            return new ResponseViewModel<UserViewModel>
            {
                Success = true,
                Data = result.Data,
                Message = "Registration successful."
            };
        }
        else
        {
            // Handle registration failure, e.g., logging or throwing an exception
            return new ResponseViewModel<UserViewModel>
            {
                Success = false,
                Message = "Registration failed. Please check your input.",
                Error = new ErrorViewModel
                {
                    Code = "REGISTRATION_ERROR",
                    Message = result?.Error?.Message ?? "An error occurred during registration. Please try again."
                }
            };
        }
    }
}
