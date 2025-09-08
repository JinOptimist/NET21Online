
namespace WebPortal.Services
{
    public interface ISourcePDFService
    {
        void UploadSource(int newsId, IFormFile source);
    }
}