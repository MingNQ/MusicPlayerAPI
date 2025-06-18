using Microsoft.AspNetCore.Identity;
using MusicPlayer.Core.Entities.Business;
using MusicPlayer.Core.Interfaces;

namespace MusicPlayer.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<ResponseViewModel<UserViewModel>> LoginAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            return new ResponseViewModel<UserViewModel>
            {
                Success = false
            };
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
        if (result.Succeeded)
        {
            return new ResponseViewModel<UserViewModel>
            {
                Success = true,
                Data = new UserViewModel 
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName!,
                    Email = user.Email!,
                    PhoneNumber = user.PhoneNumber
                }
            };
        }
        else
        {
            return new ResponseViewModel<UserViewModel>
            {
                Success = false,
                Message = "Login failed. Please check your credentials.",
                Error = new ErrorViewModel
                {
                    Code = "LOGIN_ERROR",
                    Message = "Incorrect username or password. Please check your credentials and try again."
                }
            };
        }
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<ResponseViewModel<UserViewModel>> RegisterAsync(string userName, string email, string? phoneNumber, string password)
    {
        var existingUser = await _userManager.FindByNameAsync(userName);

        if (existingUser != null)
        {
            return new ResponseViewModel<UserViewModel>
            {
                Success = false,
                Message = "User already exists.",
                Error = new ErrorViewModel
                {
                    Code = "USER_ALREADY_EXISTS",
                    Message = "A user with this username already exists."
                }
            };
        }

        var identityUser = new IdentityUser
        {
            UserName = userName,
            Email = email,
            PhoneNumber = phoneNumber
        };

        var result = await _userManager.CreateAsync(identityUser, password);

        if (result.Succeeded)
        {
            return new ResponseViewModel<UserViewModel>
            {
                Success = true,
                Data = new UserViewModel 
                { 
                    Id = Guid.NewGuid(),
                    Username = userName, 
                    Email = email, 
                    PhoneNumber = phoneNumber 
                },
                Message = "Registration successful."
            };
        }
        else
        {
            return new ResponseViewModel<UserViewModel>
            {
                Success = false,
                Message = "Registration failed.",
                Error = new ErrorViewModel
                {
                    Code = "REGISTRATION_ERROR",
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                }
            };
        }
    }
}
