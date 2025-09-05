
namespace WebPortal.Services
{
    public interface ICompShopFileService : IFileService
    {
        void UploadAvatar(IFormFile file, int? deviceId);
    }
}