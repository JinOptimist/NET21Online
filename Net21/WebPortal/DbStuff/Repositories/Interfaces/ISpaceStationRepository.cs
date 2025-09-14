using WebPortal.DbStuff.Models;
using WebPortal.Models.SpaceStation;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface ISpaceStationRepository : IBaseRepository<SpaceNews>
    {
        List<SpaceNews> FirstNews();
        bool IsUniqTitle(string? title);
        List<AuthorStatisticsViewModel> GetAuthorStatistics();
    }
}