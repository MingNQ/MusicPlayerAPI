namespace MusicPlayer.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]s")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{id}")]
    public IActionResult GetUserProfile(Guid id)
    {
        return Ok("Called GetUserProfile");
    }

    [HttpGet]
    public IActionResult GetUsers([AsParameters] PaginationRequest paginationRequest)
    {
        return Ok(new PaginationResponse<User>(
            paginationRequest.Page,
            paginationRequest.PageSize,
            0,
            []
            ));
    }

    [HttpPost]
    public IActionResult CreateUser(UserCreateViewModel model)
    {
        return Ok("Called GetUsers");
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(Guid id, UserUpdateViewModel model)
    {
        return Ok("Called UpdateUser");
    }
}
