
using CoffeProductMinimalApi.DbStuff;
using CoffeProductMinimalApi.DbStuff.Models;

namespace CoffeProductMinimalApi
{
    public class CoffeService
    {
        private CoffeProductDbContext _coffeProductDb;

        public CoffeService(CoffeProductDbContext coffeProductDb)
        {
            _coffeProductDb = coffeProductDb;
        }

        public List<string> GetNameProduct()

        {
            return _coffeProductDb.CoffeProducts.Select(x => x.Name).ToList();
        }

        public int CreateCoffe(string name, string url)
        {
            var coffe = new CoffeProduct 
            { 
                Name = name,
                Url = url
            };
            _coffeProductDb.CoffeProducts.Add(coffe);
            _coffeProductDb.SaveChanges();

            return coffe.Id;
        }


    }
}
