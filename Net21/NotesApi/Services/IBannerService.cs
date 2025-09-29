using NotesApi.DTOs;

namespace NotesApi.Services;

public interface IBannerService
{
    IEnumerable<BannerDtoV1> GetAllBannersV1();
    BannerDtoV1? GetBannerByIdV1(int id);
    BannerDtoV1 CreateBannerV1(CreateBannerV1 request);
    BannerDtoV1? UpdateBannerV1(int id, UpdateBannerV1 request);
    bool DeleteBanner(int id);
}