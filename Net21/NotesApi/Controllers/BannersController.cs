using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NotesApi.DTOs;
using NotesApi.Services;

namespace NotesApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class BannersController : ControllerBase
{
    private readonly IBannerService _bannerService;

    public BannersController(IBannerService bannerService)
    {
        _bannerService = bannerService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BannerDtoV1>), StatusCodes.Status200OK)]
    public IActionResult GetBanners()
    {
        var banners = _bannerService.GetAllBannersV1();
        return Ok(banners);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BannerDtoV1), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetBannerById(int id)
    {
        var banner = _bannerService.GetBannerByIdV1(id);
        return banner is not null ? Ok(banner) : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(BannerDtoV1), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public IActionResult CreateBanner([FromBody] CreateBannerV1 request)
    {
        var banner = _bannerService.CreateBannerV1(request);
        return CreatedAtAction(nameof(GetBannerById), new { id = banner.Id }, banner);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(BannerDtoV1), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateBanner(int id, [FromBody] UpdateBannerV1 request)
    {
        var banner = _bannerService.UpdateBannerV1(id, request);
        return banner is not null ? Ok(banner) : NotFound();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteBanner(int id)
    {
        var success = _bannerService.DeleteBanner(id);
        return success ? NoContent() : NotFound();
    }
}