using RentalCarsMinimalApi.DBContext;
using RentalCarsMinimalApi.DBContext.Models;

namespace RentalCarsMinimalApi
{
    public class RentCarsService
    {
        private RentalCarDBContext _rentalCarDBContext;

        public RentCarsService(RentalCarDBContext rentalCarDBContext)
        {
            _rentalCarDBContext = rentalCarDBContext;
        }

        public int CreateRentalCar(string name, string description, string url, int price)
        {
            var rentalCar = new RentalCar
            {
                Name = name,
                Description = description,
                Url = url,
                Price = price
            };

            _rentalCarDBContext.RentalCars.Add(rentalCar);
            _rentalCarDBContext.SaveChanges();
            return rentalCar.Id;
        }

        public List<string> GetCarsNames()
        {
            return _rentalCarDBContext.RentalCars.Select(x => x.Name).ToList();
        }

        public List<string> GetUrls()
        {
            return _rentalCarDBContext.RentalCars.Select(x => x.Url).ToList();
        }

        public List<RentalCar> GetRentalCars()
        {
            return _rentalCarDBContext
                .RentalCars
                .Select(x => new RentalCar
                {
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Url = x.Url
                })
                .ToList();
        }
    }
}
