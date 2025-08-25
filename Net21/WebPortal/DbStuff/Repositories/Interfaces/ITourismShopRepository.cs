using WebPortal.DbStuff.Models.Tourism;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface ITourismShopRepository : IBaseRepository<TourismShop>
    {
        List<TourismShop> GetShopItemWithAuthor();
    }
}