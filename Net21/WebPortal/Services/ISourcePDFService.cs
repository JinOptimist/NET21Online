
namespace WebPortal.Services
{
    public interface ISourcePDFService
    {
        Task DeleteSource(int newsId);
        string GetSourceUrlForNews(int newsId);
        bool SourceExists(int newsId);
        Task UploadSource(int newsId, IFormFile source);
    }
}