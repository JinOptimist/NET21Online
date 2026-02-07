
namespace WebPortal.Services
{
    public interface IFileService
    {
        void ReplaceAvatarToDefault(int userId);
        void UploadAvatar(IFormFile file, int? userId = null);
        string GetPathToAvatar(int id);
        void RemoveOutdateAvatarFile();
    }
}