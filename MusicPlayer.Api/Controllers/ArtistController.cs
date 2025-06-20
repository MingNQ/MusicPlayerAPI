using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace MusicPlayer.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]s")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ArtistController : ControllerBase
{
    private readonly ILogger<ArtistController> _logger;

    public ArtistController(ILogger<ArtistController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{id}")]
    public IActionResult GetArtistProfile(Guid id)
    {
        return Ok("Called");
    }

    [HttpPut("{id}")]
    public IActionResult UpdateArtist(Guid id, ArtistUpdateViewModel model)
    {
        return Ok("Called");
    }

    [HttpGet]
    public IActionResult GetArtists()
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult CreateArtist(ArtistCreateViewModel model)
    {
        return Ok("Called");
    }

    [Route("{id}/albums")]
    [HttpGet]
    public IActionResult GetArtistAlbum(Guid id)
    {
        return Ok("Called");
    }

    [Route("{id}/tracks")]
    [HttpGet]
    public IActionResult GetArtistTopTracks(Guid id)
    {
        return Ok("Called");
    }

}
