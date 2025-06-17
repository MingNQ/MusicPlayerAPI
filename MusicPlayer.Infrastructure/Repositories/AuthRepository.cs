using Microsoft.AspNetCore.Identity;
using MusicPlayer.Core.Entities.Business;
using MusicPlayer.Core.Entities.General;
using MusicPlayer.Core.Interfaces;

namespace MusicPlayer.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    //private readonly UserManager<User> _userManager;
    //private readonly SignInManager<User> _signInManager;

    //public AuthRepository(UserManager<User> userManager, SignInManager<User> signInManager)
    //{
    //    _userManager = userManager;
    //    _signInManager = signInManager;
    //}

    public async Task<ResponseViewModel<UserViewModel>> LoginAsync(string userName, string password)
    {
        //var user = await _userManager.FindByNameAsync(userName);

        //if (user == null)
        //{
        //    return new ResponseViewModel<UserViewModel>
        //    {
        //        Success = false
        //    };
        //}

        //var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
        //if (result.Succeeded)
        //{
        //    return new ResponseViewModel<UserViewModel>
        //    {
        //        Success = true,
        //        Data = new UserViewModel { }
        //    };
        //}
        //else
        //{
        //    return new ResponseViewModel<UserViewModel>
        //    {
        //        Success = false
        //    };
        //}

        throw new NotImplementedException();
    }

    public async Task LogoutAsync()
    {
        //await _signInManager.SignOutAsync();

        throw new NotImplementedException();
    }
}
