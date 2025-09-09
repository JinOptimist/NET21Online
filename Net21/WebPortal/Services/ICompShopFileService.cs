
namespace WebPortal.Services
{
    public interface ICompShopFileService : IFileService
    {
        void UploadAvatar(IFormFile file, int? deviceId);
        string CreateImagePath(string oldPath);
        string GetPathToDeviceFolder();
        string GetPathToDevice(int deviceId);
    }
}