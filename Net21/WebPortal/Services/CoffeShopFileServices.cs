using Microsoft.AspNetCore.Hosting;

namespace WebPortal.Services
{
    public class CoffeShopFileServices : ICoffeShopFileServices
    {
        private IWebHostEnvironment _webHostEnvironment;

        private const string FON_IMG_FOLDER = "images/coffeshop/imagefon";
        private const string DEFAUL_IMG = "default.jpg";
        private const long MAX_FILE_SIZE = 5*1024*1024; //5MB Files
        private static readonly string[] ALLOWED_EXTENSIONS = { ".jpg", ".jpeg", ".png" };





        public CoffeShopFileServices(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void UploudFonCoffeShop(IFormFile file)
        {
            ValidateFile(file);

            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var uploadFolder = Path.Combine(wwwRootPath, FON_IMG_FOLDER);

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }


            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var path = Path.Combine(uploadFolder, fileName);

            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                file.CopyToAsync(stream);
            }
        }


        private void ValidateFile(IFormFile file) 
        {
            if (file == null || file.Length == 0)
            { 
                throw new ArgumentException("File is required"); 
            }

            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!ALLOWED_EXTENSIONS.Contains(extension))
            {
                throw new Exception($"Only {string.Join(", ", ALLOWED_EXTENSIONS)} are allowed.");
            }

            if (file.Length>MAX_FILE_SIZE)
            {
                throw new Exception($"Only {string.Join(", ", ALLOWED_EXTENSIONS)} are allowed.");
            }
        }

        public List<string> GetFonGallery()
        {
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, FON_IMG_FOLDER);
            var folderRequestPath = $"/{FON_IMG_FOLDER}";

            if (!Directory.Exists(folderPath))
            { 
                return GetDefaultImageList(folderRequestPath);
            }

            var files = Directory.GetFiles(folderPath)
                .Select(Path.GetFileName)
                .Where(f => !string.IsNullOrEmpty(f) &&
                           !f.Equals(DEFAUL_IMG, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return files.Any()
                ? files.Select(file => $"{folderRequestPath}/{file}").ToList()
                : GetDefaultImageList(folderRequestPath);
        }

        private List<string> GetDefaultImageList(string folderRequestPath)
        {
            return new List<string> { $"{folderRequestPath}/{DEFAUL_IMG}" };
        }

        public void RemoveImageSlider(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) ||
                            fileName.Equals(DEFAUL_IMG, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, FON_IMG_FOLDER, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }


    }
}
