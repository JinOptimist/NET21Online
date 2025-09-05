

using Microsoft.AspNetCore.Hosting;

namespace WebPortal.Services
{
    public class CompShopFileService : FileService, ICompShopFileService
    {
        public CompShopFileService(IWebHostEnvironment webHostEnvironment, AuthService authService) : base(webHostEnvironment, authService)
        {
        }

        public override void UploadAvatar(IFormFile file, int? deviceId)
        {
            if (deviceId == null)
            {
                throw new ArgumentNullException(nameof(deviceId));
            }

            var path = GetPathToAvatar(deviceId!.Value);

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

        protected override string GetPathToAvatarFolder()
        {
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var path = Path.Combine(wwwRootPath, "images", "CompShop", "DevicePhoto");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}
