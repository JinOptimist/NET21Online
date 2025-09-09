using System.IO;

namespace WebPortal.Services
{
    public class FileService : IFileService
    {
        public const string DEFAULT_AVATAR_NAME = "defaultAvatar.jpg";

        private IWebHostEnvironment _webHostEnvironment;
        private AuthService _authService;

        public FileService(IWebHostEnvironment webHostEnvironment, AuthService authService)
        {
            _webHostEnvironment = webHostEnvironment;
            _authService = authService;
        }

        public void UploadAvatar(IFormFile file)
        {
            var userId = _authService.GetId();
            var path = GetPathToAvatar(userId);

            var fileExtension = Path.GetExtension(file.FileName);
            if (fileExtension != ".jpg")
            {
                throw new Exception("not a jpg");
            }

            var mb = 1024 * 1024;
            if (file.Length > 5 * mb)
            {
                throw new Exception("To big. Can save more than 5Mb");
            }

            using (var fileStreamOnOurServer = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var streamFromClientFileSystem = file.OpenReadStream())
                {
                    streamFromClientFileSystem.CopyToAsync(fileStreamOnOurServer).Wait();
                }
            }
        }

        public string GetPathToDefaultAvatar()
        {
            return Path.Combine(GetPathToAvatarFolder(), DEFAULT_AVATAR_NAME);
        }

        private string GetPathToAvatarFolder()
        {
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var path = Path.Combine(wwwRootPath, "images", "avatars");
            return path;
        }

        public void ReplaceAvatarToDefault(int userId)
        {
            var pathToAvatar = GetPathToAvatar(userId);
            File.Delete(pathToAvatar);
            File.Copy(GetPathToDefaultAvatar(), pathToAvatar);
        }

        private string GetPathToAvatar(int userId)
        {
            var fileName = $"{userId}.jpg";
            var path = Path.Combine(GetPathToAvatarFolder(), fileName);
            return path;
        }
        
        public void UploadFile(IFormFile file)
        {
        }
    }
}
