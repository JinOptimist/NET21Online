using WebPortal.DbStuff.Models;
using WebPortal.Enum;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface IFestivalRepository : IBaseRepository<Festival>
    {
        List<Festival> GetAllWithGirls();
        List<Festival> GetByTheme(FestivalTheme theme);
        List<Festival> GetUpcoming(DateTime fromDate);
    }
}
