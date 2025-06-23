using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace MusicPlayer.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]s")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AlbumController : ControllerBase
{
    private readonly IAlbumService _albumService;
    private readonly ILogger<AlbumController> _logger;

    public AlbumController(IAlbumService albumService, ILogger<AlbumController> logger)
    {
        _albumService = albumService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAlbumAsync(Guid id)
    {
        var album = await _albumService.GetAlbumByIdAsync(id);
        if (album == null)
        {
            return NotFound(new ResponseViewModel<AlbumViewModel>
            {
                Success = false,
                Message = "Album not found."
            });
        }

        return Ok(new ResponseViewModel<AlbumViewModel>
        {
            Success = true,
            Message = "Album retrieved successfully.",
            Data = album
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAlbums(int? page, int? pageSize)
    {
        var pageValue = page ?? 1;
        var pageSizeValue = pageSize ?? 10;

        var resul = await _albumService.GetAllAsync(pageValue, pageSizeValue);

        if (resul == null || !resul.Items.Any())
        {
            return NotFound(new ResponseViewModel<PaginationResponse<AlbumViewModel>>
            {
                Success = false,
                Message = "No albums found."
            });
        }

        return Ok(new ResponseViewModel<PaginationResponse<AlbumViewModel>>
        {
            Success = true,
            Message = "Albums retrieved successfully.",
            Data = resul
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateAlbumAsync([FromBody] AlbumCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var createdAlbum = await _albumService.CreateAlbumAsync(model);
                var response = new ResponseViewModel<AlbumViewModel>
                {
                    Success = true,
                    Message = "Album created successfully.",
                    Data = createdAlbum
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the album.");
                return StatusCode(500, new ResponseViewModel<AlbumViewModel>
                {
                    Success = false,
                    Message = "An error occurred while creating the album.",
                    Error = new ErrorViewModel
                    {
                        Code = "INTERNAL_SERVER_ERROR",
                        Message = ex.Message
                    }
                });
            }
        }

        return BadRequest(new ResponseViewModel<AlbumViewModel>
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAlbumAsync(Guid id, [FromBody] AlbumUpdateViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _albumService.UpdateAlbumAsync(id, model);
                return Ok(new ResponseViewModel<AlbumViewModel>
                {
                    Success = true,
                    Message = "Album updated successfully."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the album.");
                return StatusCode(500, new ResponseViewModel<AlbumViewModel>
                {
                    Success = false,
                    Message = "An error occurred while updating the album.",
                    Error = new ErrorViewModel
                    {
                        Code = "INTERNAL_SERVER_ERROR",
                        Message = ex.Message
                    }
                });
            }
        }

        return BadRequest(new ResponseViewModel<AlbumViewModel>
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

    [HttpDelete("{id}")]
    public IActionResult DeleteAlbumAsync(Guid id)
    {
        try
        {
            _albumService.DeleteAlbumAsync(id).Wait();
            return Ok(new ResponseViewModel<AlbumViewModel>
            {
                Success = true,
                Message = "Album deleted successfully."
            });
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError(ex, "Album not found.");
            return NotFound(new ResponseViewModel<AlbumViewModel>
            {
                Success = false,
                Message = "Album not found."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the album.");
            return StatusCode(500, new ResponseViewModel<AlbumViewModel>
            {
                Success = false,
                Message = "An error occurred while deleting the album.",
                Error = new ErrorViewModel
                {
                    Code = "INTERNAL_SERVER_ERROR",
                    Message = ex.Message
                }
            });
        }
    }
}
