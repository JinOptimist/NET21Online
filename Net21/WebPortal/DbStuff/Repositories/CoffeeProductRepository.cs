using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models.CoffeShop;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.CoffeShop;

namespace WebPortal.DbStuff.Repositories
{
    public class CoffeeProductRepository : BaseRepository<CoffeeProduct>, ICoffeeProductRepository
    {
        public CoffeeProductRepository(WebPortalContext portalContext) : base(portalContext)
        {
            _portalContext = portalContext;
        }

        public IEnumerable<CoffeeProduct> GetAllWithAuthors()
        {
            return _dbSet
                .Include(x => x.AuthorAdd)
                .ToList();
        }

        public List<CoffeeDetailViewModel> GetCoffeeDetail()
        {
            var coffeeDetail = @"
            SELECT 
                U.UserName AS AuthorName,
                CF.Name AS CoffeeName,
                CF.Cell AS Price
            FROM CoffeeProducts CF
                LEFT JOIN Users U ON U.Id = CF.AuthorId
            ORDER BY U.UserName, CF.Name;";

            return _portalContext
                .Database
                .SqlQueryRaw<CoffeeDetailViewModel>(coffeeDetail)
                .ToList();
        }

        public List<CoffeeSummaryViewModel> GetCoffeeSummary() 
        {
            var coffeeDetaila = @"
            SELECT 
                U.UserName AS AuthorName,
                COUNT(*) AS TotalCoffees
            FROM CoffeeProducts CF
                LEFT JOIN Users U ON U.Id = CF.AuthorId
                GROUP BY U.UserName
            ORDER BY U.UserName;";

            return _portalContext
                .Database
                .SqlQueryRaw<CoffeeSummaryViewModel>(coffeeDetaila)
                .ToList();


        }




    }
}

