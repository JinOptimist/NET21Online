using NotesApi.DbStuff;
using NotesApi.DbStuff.Models;
using NotesApi.DTOs;

namespace NotesApi.Services;

public class BannerService : IBannerService
{
    private readonly NotesApiDbContext _context;

    public BannerService(NotesApiDbContext context)
    {
        _context = context;
    }

    public IEnumerable<BannerDtoV1> GetAllBannersV1()
    {
        var banners = _context.Banners
            .Select(b => new BannerDtoV1(b.Id, b.Name, b.Url, b.ImageUrl, b.CreateDate))
            .ToList();

        return banners;
    }

    public BannerDtoV1? GetBannerByIdV1(int id)
    {
        var banner = _context.Banners
            .Where(b => b.Id == id)
            .Select(b => new BannerDtoV1(b.Id, b.Name, b.Url, b.ImageUrl, b.CreateDate))
            .FirstOrDefault();

        return banner;
    }

    public BannerDtoV1 CreateBannerV1(CreateBannerV1 request)
    {
        var banner = new Banner
        {
            Name = request.Name,
            Url = request.Url,
            ImageUrl = request.ImageUrl,
            CreateDate = DateTime.UtcNow,
            UpdateDate = DateTime.UtcNow
        };

        _context.Banners.Add(banner);
        _context.SaveChangesAsync();

        return new BannerDtoV1(banner.Id, banner.Name, banner.Url, banner.ImageUrl, banner.CreateDate);
    }

    public BannerDtoV1? UpdateBannerV1(int id, UpdateBannerV1 request)
    {
        var banner = _context.Banners.Find(id);
        if (banner == null)
            return null;

        banner.Name = request.Name;
        banner.Url = request.Url;
        banner.ImageUrl = request.ImageUrl;
        banner.UpdateDate = DateTime.UtcNow;

        _context.SaveChangesAsync();

        return new BannerDtoV1(banner.Id, banner.Name, banner.Url, banner.ImageUrl, banner.CreateDate);
    }
    
    public bool DeleteBanner(int id)
    {
        var banner = _context.Banners.Find(id);
        if (banner == null)
            return false;

        _context.Banners.Remove(banner);
        var result = _context.SaveChanges();
        return result > 0;
    }
}