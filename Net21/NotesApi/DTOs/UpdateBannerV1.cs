namespace NotesApi.DTOs;

public record UpdateBannerV1(
    string Name,
    string Url,
    string ImageUrl);