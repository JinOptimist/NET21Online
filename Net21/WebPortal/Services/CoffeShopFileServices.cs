using Microsoft.AspNetCore.Hosting;

namespace WebPortal.Services
{
    public class CoffeShopFileServices : ICoffeShopFileServices
    {
        private IWebHostEnvironment _webHostEnvironment;

        public CoffeShopFileServices(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void UploudFonCoffeShop(IFormFile file)
        {
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var uploadFolder = Path.Combine(wwwRootPath, "images", "coffeshop", "imagefon");

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }


            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var path = Path.Combine(uploadFolder, fileName);

            var extension = Path.GetExtension(file.FileName).ToLower();
            if (extension != ".jpg" && extension != ".png" && extension != ".jpeg")
            {
                throw new Exception("Only JPG, JPEG, PNG are allowed.");
            }

            var mb = 1024 * 1024;
            if (file.Length > 5 * mb)
            {
                throw new Exception("Too big image. Max 5Mb allowed.");
            }

            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                file.CopyToAsync(stream);
            }
        }
        public List<string> GetFonGallery()
        {

            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "coffeshop", "imagefon");
            var folderRequestPath = "/images/coffeshop/imagefon";

            if (!Directory.Exists(folderPath))
            {
                return new List<string> { $"{folderRequestPath}/default.jpg" };
            }

            var files = Directory.GetFiles(folderPath)
                .Select(Path.GetFileName)
                .Where(f => !string.IsNullOrEmpty(f))
                .ToList();

            if (files.Count == 0)
            {
                return new List<string> { $"{folderRequestPath}/default.jpg" };
            }

            var nonDefaultFiles = files
                .Where(f => !f.Equals("default.jpg", StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (nonDefaultFiles.Any())
            {
                return nonDefaultFiles.Select(file => $"{folderRequestPath}/{file}").ToList();
            }

            return new List<string> { $"{folderRequestPath}/default.jpg" };

        }

        public void RemoveImageSlider(string fileName)
        {

            if (string.IsNullOrEmpty(fileName) ||
            fileName.Equals("default.jpg", 
            StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var folderPath = Path.Combine(wwwRootPath, "images", "coffeshop", "imagefon");
            var filePath = Path.Combine(folderPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }


    }
}
