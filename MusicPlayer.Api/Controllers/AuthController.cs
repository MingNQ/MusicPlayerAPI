namespace MusicPlayer.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var result = await _authService.LoginAsync(model.Username, model.Password);
        if (result.Success)
        {
            return Ok(result);
        }
        else
        {
            return Unauthorized(result.Error);
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return Ok(new { Message = "Logout successful." });
    }
}
