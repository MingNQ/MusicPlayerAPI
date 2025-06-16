﻿using MusicPlayer.Api.Models;
using MusicPlayer.Core.Entities.General;

namespace MusicPlayer.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]s")]
[ApiController]
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
    public IActionResult GetArtists([AsParameters]PaginationRequest paginationRequest)
    {
        return Ok(new PaginationResponse<User>(
             paginationRequest.Page,
             paginationRequest.PageSize,
             0,
             []
             ));
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
