using System.IO;

namespace WebPortal.Services
{
    public class TourismFilesService : ITourismFilesService
    {
        private IWebHostEnvironment _webHostEnvironment;
        private IAuthService _authService;

        public TourismFilesService(IWebHostEnvironment webHostEnvironment, IAuthService authService)
        {
            _webHostEnvironment = webHostEnvironment;
            _authService = authService;
        }

        public string UploadImage(IFormFile file)
        {
            var userId = _authService.GetId();
            var fileName = $"{userId}+{DateTime.UtcNow:yyyyMMddHHmmss}.jpg";
            var path = Path.Combine(GetPathToTourFolderImages(), fileName);

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
            var relativePath = "/images/tourism/" + fileName;
            return relativePath;
            //
            //Need a comment about return of this methot
            //
        }

        private string GetPathToTourFolderImages()
        {
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var path = Path.Combine(wwwRootPath, "images", "tourism");
            return path;
        }
    }
}
