using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusicPlayer.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly AppSettings _appSettings;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, IOptions<AppSettings> appSettings, ILogger<AuthController> logger)
    {
        _authService = authService;
        _appSettings = appSettings.Value;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _authService.LoginAsync(model.Username, model.Password);
            if (result.Success)
            {
                var token = GenerateToken(result);

                _logger.LogInformation("User {Username} logged in successfully.", model.Username);
                return Ok(new ResponseViewModel<AuthResultViewModel>
                {
                    Success = true,
                    Data = token,
                    Message = "Login successful."
                });
            }

            _logger.LogWarning("Login failed for user {Username}: {Message}", model.Username, result.Message);
            return BadRequest(result);
        }
        else
        {
            _logger.LogWarning("Invalid login attempt for user {Username}: {Errors}", model.Username, ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return BadRequest(new ResponseViewModel
            {
                Success = false,
                Message = "Invalid input",
                Error = new ErrorViewModel
                {
                    Code = "INPUT_VALIDATION_ERROR",
                    Message = "Please check the input data."
                }
            });
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        _logger.LogInformation("User logged out successfully.");
        return Ok(new { Message = "Logout successful." });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    {
        if (!ModelState.IsValid || model.Password != model.ConfirmPassword)
        {
            return BadRequest(new ResponseViewModel
            {
                Success = false,
                Message = "Invalid input",
                Error = new ErrorViewModel
                {
                    Code = "INPUT_VALIDATION_ERROR",
                    Message = "Please check the input data."
                }
            });
        }

        var result = await _authService.RegisterAsync(model.UserName, model.Email, model.PhoneNumber, model.Password);

        if (!result.Success)
        {
            _logger.LogWarning("Registration failed for user {Username}: {Message}", model.UserName, result.Message);
            return BadRequest(result);
        }

        _logger.LogInformation("User {Username} registered successfully.", model.UserName);

        return Ok(new ResponseViewModel<AuthResultViewModel>
        {
            Success = true,
            Message = "Registration successful."
        });
    }

    private AuthResultViewModel GenerateToken(ResponseViewModel<UserViewModel> result)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.JwtConfig.Key);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Aud, _appSettings.JwtConfig.Audience),
            new Claim(JwtRegisteredClaimNames.Iss, _appSettings.JwtConfig.Issuer),
            new Claim(JwtRegisteredClaimNames.Sub, result!.Data!.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_appSettings.JwtConfig.ExpirationMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = jwtHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtHandler.WriteToken(token);

        return new AuthResultViewModel
        {
            AccessToken = jwtToken,
            Success = true,
        };
    }
}
