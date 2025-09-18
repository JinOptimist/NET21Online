using WebPortal.DbStuff.Models.CoffeShop;
using WebPortal.Models.CoffeShop;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface ICoffeeProductRepository : IBaseRepository<CoffeeProduct>
    {
        IEnumerable<CoffeeProduct> GetAllWithAuthors();
        List<CoffeeDetailViewModel> GetCoffeeDetail();
        List<CoffeeSummaryViewModel> GetCoffeeSummary();
    }
}