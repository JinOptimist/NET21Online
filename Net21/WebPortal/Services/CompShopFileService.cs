using Microsoft.AspNetCore.Hosting;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.Services
{
    public class CompShopFileService : FileService, ICompShopFileService
    {

        public CompShopFileService(
            IWebHostEnvironment webHostEnvironment, 
            IAuthService authService,
            IUserRepositrory userRepositrory) 
            : base(webHostEnvironment, authService, userRepositrory)
        {
        }

        public override void UploadAvatar(IFormFile file, int? deviceId)
        {
            if (deviceId == null)
            {
                throw new ArgumentNullException(nameof(deviceId));
            }

            var path = GetPathToDevice(deviceId!.Value);

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

        public string GetPathToDeviceFolder()
        {
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var path = Path.Combine(wwwRootPath, "images", "CompShop", "DevicePhoto");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public string GetPathToDevice(int deviceId)
        {
            var fileName = $"{deviceId}.jpg";
            var path = Path.Combine(GetPathToDeviceFolder(), fileName);
            return path;
        }

        public string CreateImagePath(string oldPath)
        {
            var index = oldPath.IndexOf("wwwroot");

            if (index < 0)
            {
                throw new Exception("Wrong path to image");
            }

            var newPath = oldPath.Substring(index + 7); // 7 - wwwroot.Length

            if (newPath.StartsWith("\\") || newPath.StartsWith("/"))
            {
                newPath = newPath.Substring(1);
            }

            return "/" + newPath.Replace("\\", "/");
        }

        protected override string GetPathToAvatarFolder()
        {
            throw new Exception("Use 'GetPathToDeviceFolder' method");
        }

        
    }
}
