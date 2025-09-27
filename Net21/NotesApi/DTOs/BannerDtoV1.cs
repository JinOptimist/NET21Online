namespace NotesApi.DTOs;

public record BannerDtoV1(
    int Id,
    string Name,
    string Url,
    string ImageUrl,
    DateTime CreateDate);