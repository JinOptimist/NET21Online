using Microsoft.AspNetCore.Hosting;

namespace WebPortal.Services
{
    public class SourcePDFService : ISourcePDFService
    {
        private IWebHostEnvironment _webHostEnvironment;
        private AuthService _authService;

        public SourcePDFService(IWebHostEnvironment webHostEnvironment, AuthService authService)
        {
            _webHostEnvironment = webHostEnvironment;
            _authService = authService;
        }

        public void UploadSource(int newsId, IFormFile source)
        {
            var fileName = $"{newsId}.pdf";
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var path = System.IO.Path.Combine(wwwRootPath, "documents", fileName);

            var fileExtention = Path.GetExtension(source.FileName);
            if (fileExtention != ".pdf")
            {
                throw new Exception("Wrong document format, PDF requied");
            }
            var mb = 1024 * 1024;
            if (source.Length > 20 * mb)
            {
                throw new Exception("Wrong document size, 20 mb max"); ;
            }

            using (var fileStreamOnOurServer = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var streamFromClientFileSystem = source.OpenReadStream())
                {
                    streamFromClientFileSystem.CopyToAsync(fileStreamOnOurServer).Wait();
                }
            }
        }
    }
}
