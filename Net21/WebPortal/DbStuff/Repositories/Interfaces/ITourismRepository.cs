using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Models.Tourism;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface ITourismRepository : IBaseRepository<Tourism>
    {
        List<Tourism> GetPopularListTitles();
    }
}