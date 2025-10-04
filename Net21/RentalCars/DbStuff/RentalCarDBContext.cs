using Microsoft.EntityFrameworkCore;
using RentalCarsMinimalApi.DBContext.Models;

namespace RentalCarsMinimalApi.DBContext
{
    public class RentalCarDBContext: DbContext
    {
        public RentalCarDBContext(DbContextOptions<RentalCarDBContext> options)
           : base(options) { }

        public DbSet<RentalCar> RentalCars { get; set; }
    }
}
