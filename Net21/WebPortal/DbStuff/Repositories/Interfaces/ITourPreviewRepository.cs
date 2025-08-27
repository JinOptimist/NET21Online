using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Models.Tourism;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface ITourPreviewRepository : IBaseRepository<TourPreview>
    {
        List<TourPreview> GetPopularListTitles();
    }
}