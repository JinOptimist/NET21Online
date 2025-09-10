using WebPortal.DbStuff.Models.CompShop;

namespace WebPortal.DbStuff.Repositories.Interfaces.CompShop
{
    public interface INewsRepository : IBaseRepository<News>
    {
        List<News> GetFirstNews(int count);
    }
}