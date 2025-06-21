using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace MusicPlayer.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]s")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ArtistController : ControllerBase
{
    private readonly ILogger<ArtistController> _logger;
    private readonly IArtistService _artistService;

    public ArtistController(ILogger<ArtistController> logger, IArtistService artistService)
    {
        _logger = logger;
        _artistService = artistService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetArtistProfile(Guid id)
    {
        var artist = await _artistService.GetArtistByIdAsync(id);
        if (artist == null)
        {
            return NotFound(new ResponseViewModel<ArtistViewModel>
            {
                Success = false,
                Message = "Artist not found."
            });
        }

        var response = new ResponseViewModel<ArtistViewModel>
        {
            Success = true,
            Message = "Artist profile retrieved successfully.",
            Data = artist
        };

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateArtist(Guid id, ArtistUpdateViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _artistService.UpdateArtistAsync(id, model);

                return Ok(new ResponseViewModel<ArtistViewModel>
                {
                    Success = true,
                    Message = "Artist updated successfully."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the artist.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseViewModel<ArtistViewModel>
                {
                    Success = false,
                    Message = "An error occurred while updating the artist.",
                    Error = new ErrorViewModel
                    {
                        Code = "ERROR_CODE",
                        Message = ex.Message
                    }
                });
            }
        }

        return BadRequest(new ResponseViewModel<ArtistViewModel>
        {
            Success = false,
            Message = "Invalid model state.",
            Error = new ErrorViewModel
            {
                Code = "INVALID_MODEL",
                Message = "The provided model is invalid."
            }
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetArtists(int? page, int? pageSize)
    {
        var result = await _artistService.GetArtistsAsync(page ?? 1, pageSize ?? 10);

        if (result == null || !result.Items.Any())
        {
            return NotFound(new ResponseViewModel<PaginationResponse<ArtistViewModel>>
            {
                Success = false,
                Message = "No artists found."
            });
        }

        var response = new ResponseViewModel<PaginationResponse<ArtistViewModel>>
        {
            Success = true,
            Message = "Artists retrieved successfully.",
            Data = result
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateArtist(ArtistCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var result = await _artistService.CreateArtistAsync(model);
                var response = new ResponseViewModel<ArtistViewModel>
                {
                    Success = true,
                    Message = "Artist created successfully.",
                    Data = result
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the artist.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseViewModel<ArtistViewModel>
                {
                    Success = false,
                    Message = "An error occurred while creating the artist.",
                    Error = new ErrorViewModel
                    {
                        Code = "ERROR_CODE",
                        Message = ex.Message
                    }
                });
            }
        }

        return BadRequest(new ResponseViewModel<ArtistViewModel>
        {
            Success = false,
            Message = "Invalid model state.",
            Error = new ErrorViewModel
            {
                Code = "INVALID_MODEL",
                Message = "The provided model is invalid."
            }
        });
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
