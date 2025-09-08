using Microsoft.AspNetCore.Hosting;

namespace WebPortal.Services
{
    public class SourcePDFService : ISourcePDFService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AuthService _authService;
        private const string DocumentsFolder = "documents";
        private const long MaxFileSize = 20 * 1024 * 1024; // 20 MB
        private const string AllowedExtension = ".pdf";

        public SourcePDFService(IWebHostEnvironment webHostEnvironment, AuthService authService)
        {
            _webHostEnvironment = webHostEnvironment;
            _authService = authService;
        }

        public void UploadSource(int newsId, IFormFile source)
        {
            ValidateFile(source);

            var path = GetFilePath(newsId);
            EnsureDocumentsDirectoryExists();

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                source.CopyTo(fileStream);
            }
        }

        public string GetSourceUrlForNews(int newsId)
        {
            var filePath = GetFilePath(newsId);

            if (File.Exists(filePath))
            {
                return $"/{DocumentsFolder}/{GetFileName(newsId)}";
            }

            throw new Exception("Something wrong with file you searching for"); ;
        }

        public void DeleteSource(int newsId)
        {
            var filePath = GetFilePath(newsId);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public bool SourceExists(int newsId)
        {
            return File.Exists(GetFilePath(newsId));
        }

        private void ValidateFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is empty");
            }

            var fileExtension = Path.GetExtension(file.FileName);
            if (!string.Equals(fileExtension, AllowedExtension, StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Wrong document format, PDF required");
            }

            if (file.Length > MaxFileSize)
            {
                throw new Exception($"Wrong document size, {MaxFileSize / (1024 * 1024)} MB max");
            }
        }

        private string GetFilePath(int newsId)
        {
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            return Path.Combine(wwwRootPath, DocumentsFolder, GetFileName(newsId));
        }

        private string GetFileName(int newsId)
        {
            return $"{newsId}.pdf";
        }

        private void EnsureDocumentsDirectoryExists()
        {
            var documentsPath = Path.Combine(_webHostEnvironment.WebRootPath, DocumentsFolder);
            if (!Directory.Exists(documentsPath))
            {
                Directory.CreateDirectory(documentsPath);
            }
        }
    }
}
