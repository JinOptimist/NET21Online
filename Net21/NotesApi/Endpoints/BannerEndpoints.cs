using Microsoft.AspNetCore.Mvc;
using NotesApi.DTOs;
using NotesApi.Services;

namespace NotesApi.Endpoints;

public static class BannerEndpoints
{
    public static void MapBannerEndpointsV1(this IEndpointRouteBuilder app)
    {
        var group = app.NewVersionedApi("Banners")
            .MapGroup("/api/v{version:apiVersion}/banners")
            .HasApiVersion(1, 0)
            .WithTags("Banners V1")
            .WithMetadata(new ApiExplorerSettingsAttribute { GroupName = "v1" });

        group.MapGet("/", GetBannersV1)
            .WithName("GetBannersV1")
            .WithSummary("Get all banners (V1)")
            .Produces<IEnumerable<BannerDtoV1>>(StatusCodes.Status200OK)
            .WithMetadata(new ApiExplorerSettingsAttribute { GroupName = "v1" });

        group.MapGet("/{id:int}", GetBannerByIdV1)
            .WithName("GetBannerByIdV1")
            .WithSummary("Get banner by ID (V1)")
            .Produces<BannerDtoV1>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithMetadata(new ApiExplorerSettingsAttribute { GroupName = "v1" });

        group.MapPost("/", CreateBannerV1)
            .WithName("CreateBannerV1")
            .WithSummary("Create new banner (V1)")
            .Accepts<CreateBannerV1>("application/json")
            .Produces<BannerDtoV1>(StatusCodes.Status201Created)
            .Produces<ValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .WithMetadata(new ApiExplorerSettingsAttribute { GroupName = "v1" });

        group.MapPut("/{id:int}", UpdateBannerV1)
            .WithName("UpdateBannerV1")
            .WithSummary("Update banner (V1)")
            .Accepts<UpdateBannerV1>("application/json")
            .Produces<BannerDtoV1>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithMetadata(new ApiExplorerSettingsAttribute { GroupName = "v1" });

        group.MapDelete("/{id:int}", DeleteBannerV1)
            .WithName("DeleteBannerV1")
            .WithSummary("Delete banner (V1)")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithMetadata(new ApiExplorerSettingsAttribute { GroupName = "v1" });
    }

    private static IResult GetBannersV1(IBannerService bannerService)
    {
        var banners = bannerService.GetAllBannersV1();
        return Results.Ok(banners);
    }

    private static IResult GetBannerByIdV1(int id, IBannerService bannerService)
    {
        var banner = bannerService.GetBannerByIdV1(id);
        return banner is not null ? Results.Ok(banner) : Results.NotFound();
    }

    private static IResult CreateBannerV1(CreateBannerV1 request, IBannerService bannerService)
    {
        var banner = bannerService.CreateBannerV1(request);
        return Results.Created($"/api/v1/banners/{banner.Id}", banner);
    }

    private static IResult UpdateBannerV1(int id, UpdateBannerV1 request, IBannerService bannerService)
    {
        var banner = bannerService.UpdateBannerV1(id, request);
        return banner is not null ? Results.Ok(banner) : Results.NotFound();
    }

    private static IResult DeleteBannerV1(int id, IBannerService bannerService)
    {
        var success = bannerService.DeleteBanner(id);
        return success ? Results.NoContent() : Results.NotFound();
    }
}