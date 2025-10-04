namespace NotesApi.DTOs;

public record CreateBannerV1(
    string Name,
    string Url,
    string ImageUrl);