using System.IO;

namespace WebPortal.Services
{
    public class FileService : IFileService
    {
        public const string DEFAULT_AVATAR_NAME = "defaultAvatar.jpg";

        protected IWebHostEnvironment _webHostEnvironment;
        private IAuthService _authService;

        public FileService(IWebHostEnvironment webHostEnvironment, IAuthService authService)
        {
            _webHostEnvironment = webHostEnvironment;
            _authService = authService;
        }

        public virtual void UploadAvatar(IFormFile file, int? userId = null)
        {
            userId = _authService.GetId();
            var path = GetPathToAvatar(userId!.Value);

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

        protected virtual string GetPathToAvatarFolder()
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

        public virtual string GetPathToAvatar(int userId)
        {
            var fileName = $"{userId}.jpg";
            var path = Path.Combine(GetPathToAvatarFolder(), fileName);
            return path;
        }
    }
}
