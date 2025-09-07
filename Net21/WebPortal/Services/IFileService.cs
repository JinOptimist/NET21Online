
namespace WebPortal.Services
{
    public interface IFileService
    {
        void ReplaceAvatarToDefault(int userId);
        void UploadAvatar(IFormFile file);
    }
}