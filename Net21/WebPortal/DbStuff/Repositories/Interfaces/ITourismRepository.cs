using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface ITourismRepository : IBaseRepository<Tourism>
    {
        List<Tourism> GetPopularListTitles();
    }
}