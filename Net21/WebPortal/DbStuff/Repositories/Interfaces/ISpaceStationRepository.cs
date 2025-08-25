using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface ISpaceStationRepository : IBaseRepository<SpaceNews>
    {
        List<SpaceNews> FirstNews();
        bool IsUniqTitle(string? title);
    }
}