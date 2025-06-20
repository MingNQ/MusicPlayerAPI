using System.Threading.Tasks;

namespace MusicPlayer.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]s")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserProfile(Guid id)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id);

            var response = new ResponseViewModel<UserViewModel>
            {
                Success = true,
                Message = "User profile retrieved successfully.",
                Data = user
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving user profile.");
            var errorResponse = new ResponseViewModel<UserViewModel>
            {
                Success = false,
                Message = "An error occurred while retrieving user profile.",
                Error = new ErrorViewModel
                {
                    Code = "ERROR_CODE",
                    Message = ex.Message
                }
            };
            return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers(int? page, int? pageSize)
    {
        try
        {
            int pageValue = page ?? 1;
            int pageSizeValue = pageSize ?? 10;

            var users = await _userService.GetUsersAsync(pageValue, pageSizeValue);

            var response = new ResponseViewModel<PaginationResponse<UserViewModel>>
            {
                Success = true,
                Message = "Users retrieved successfully.",
                Data = users
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving users.");

            var errorResponse = new ResponseViewModel<IEnumerable<UserViewModel>>
            {
                Success = false,
                Message = "An error occurred while retrieving users.",
                Error = new ErrorViewModel
                {
                    Code = "ERROR_CODE",
                    Message = ex.Message
                }
            };

            return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var response = await _userService.CreateUserAsync(model);

                if (response.Success)
                {
                    _logger.LogInformation("User created successfully: {Username}", model.Username);
                    return Ok(response);
                }
                else
                {
                    _logger.LogWarning("User creation failed: {Message}", response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating user");

                var errorResponse = new ResponseViewModel<UserViewModel>
                {
                    Success = false,
                    Message = "An error occurred while creating user.",
                    Error = new ErrorViewModel
                    {
                        Code = "ERROR_CODE",
                        Message = ex.Message
                    }
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        return BadRequest(new ResponseViewModel<UserViewModel>
        {
            Success = false,
            Message = "Invalid input data.",
            Error = new ErrorViewModel
            {
                Code = "INPUT_VALIDATION_ERROR",
                Message = "Please check the input data."
            }
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, UserUpdateViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                model.Id = id.ToString();
                var response = await _userService.UpdateUserAsync(model);

                if (response.Success)
                {
                    _logger.LogInformation("User updated successfully: {Username}", model.Username);
                    return Ok(response);
                }
                else
                {
                    _logger.LogWarning("User update failed: {Message}", response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user");
                var errorResponse = new ResponseViewModel<UserViewModel>
                {
                    Success = false,
                    Message = "An error occurred while updating user.",
                    Error = new ErrorViewModel
                    {
                        Code = "ERROR_CODE",
                        Message = ex.Message
                    }
                };
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        return BadRequest(new ResponseViewModel<UserViewModel>
        {
            Success = false,
            Message = "Invalid input data.",
            Error = new ErrorViewModel
            {
                Code = "INPUT_VALIDATION_ERROR",
                Message = "Please check the input data."
            }
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);

            var response = new ResponseViewModel<UserViewModel>
            {
                Success = true,
                Message = "User deleted successfully."
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting user");
            var errorResponse = new ResponseViewModel<UserViewModel>
            {
                Success = false,
                Message = "An error occurred while deleting user.",
                Error = new ErrorViewModel
                {
                    Code = "ERROR_CODE",
                    Message = ex.Message
                }
            };
            return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }
}
