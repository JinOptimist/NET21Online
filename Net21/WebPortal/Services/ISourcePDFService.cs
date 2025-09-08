
namespace WebPortal.Services
{
    public interface ISourcePDFService
    {
        void DeleteSource(int newsId);
        string GetSourceUrlForNews(int newsId);
        bool SourceExists(int newsId);
        void UploadSource(int newsId, IFormFile source);
    }
}