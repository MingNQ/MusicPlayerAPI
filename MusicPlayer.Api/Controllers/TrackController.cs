using System.Threading.Tasks;

namespace MusicPlayer.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]s")]
[ApiController]
public class TrackController : ControllerBase
{
    private readonly ITrackService _trackService;
    private readonly ILogger<TrackController> _logger;

    public TrackController(ITrackService trackService, ILogger<TrackController> logger)
    {
        _trackService = trackService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTrackAsync(Guid id)
    {
        try
        {
            var track = await _trackService.GetTrackByIdAsync(id);
            if (track == null)
            {
                return NotFound(new ResponseViewModel<TrackViewModel>
                {
                    Success = false,
                    Message = "Track not found."
                });
            }
            return Ok(new ResponseViewModel<TrackViewModel>
            {
                Success = true,
                Message = "Track retrieved successfully.",
                Data = track
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving track with ID {Id}", id);
            return StatusCode(500, new ResponseViewModel<TrackViewModel>
            {
                Success = false,
                Message = "An error occurred while retrieving the track."
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetTracks(int? page, int? pageSize)
    {
        try
        {
            var pageValue = page ?? 1;
            var pageSizeValue = pageSize ?? 10;
            var result = await _trackService.GetAllTracksAsync(pageValue, pageSizeValue);
            if (result == null || !result.Items.Any())
            {
                return NotFound(new ResponseViewModel<PaginationResponse<TrackViewModel>>
                {
                    Success = false,
                    Message = "No tracks found."
                });
            }

            var response = new ResponseViewModel<PaginationResponse<TrackViewModel>>
            {
                Success = true,
                Message = "Tracks retrieved successfully.",
                Data = result
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving tracks");
            return StatusCode(500, new ResponseViewModel<PaginationResponse<TrackViewModel>>
            {
                Success = false,
                Message = "An error occurred while retrieving the tracks."
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTrackAsync([FromBody] TrackCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var createdTrack = await _trackService.CreateTrackAsync(model);
                var response = new ResponseViewModel<TrackViewModel>
                {
                    Success = true,
                    Message = "Track created successfully.",
                    Data = createdTrack
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating track");
                return StatusCode(500, new ResponseViewModel<TrackViewModel>
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }
        
        return BadRequest(new ResponseViewModel<TrackViewModel>
        {
            Success = false,
            Message = "Invalid track data.",
            Error = new ErrorViewModel
            {
                Code = "INVALID_MODEL",
                Message = "The provided track data is invalid."
            }
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTrackAsync(Guid id, [FromBody] TrackUpdateViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _trackService.UpdateTrackAsync(id, model);
                return Ok(new ResponseViewModel<string>
                {
                    Success = true,
                    Message = "Track updated successfully."
                });
            }
            catch (KeyNotFoundException knfEx)
            {
                _logger.LogWarning(knfEx, "Track with ID {Id} not found", id);
                return NotFound(new ResponseViewModel<string>
                {
                    Success = false,
                    Message = knfEx.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating track with ID {Id}", id);
                return StatusCode(500, new ResponseViewModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }

        return BadRequest(new ResponseViewModel<string>
        {
            Success = false,
            Message = "Invalid track data.",
            Error = new ErrorViewModel
            {
                Code = "INVALID_MODEL",
                Message = "The provided track data is invalid."
            }
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _trackService.DeleteTrackAsync(id);

            var response = new ResponseViewModel<string>
            {
                Success = true,
                Message = "Track deleted successfully."
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating track with ID {Id}", id);
            return StatusCode(500, new ResponseViewModel<string>
            {
                Success = false,
                Message = ex.Message,
            });
        }
    }
}
