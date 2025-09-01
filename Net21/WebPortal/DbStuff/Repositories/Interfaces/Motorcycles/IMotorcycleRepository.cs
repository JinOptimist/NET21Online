using WebPortal.DbStuff.Models.Motorcycles;

namespace WebPortal.DbStuff.Repositories.Interfaces.Motorcycles
{
    public interface IMotorcycleRepository : IBaseRepository<Motorcycle>
    {
        List<Motorcycle> GetNewMotorcycle();
    }
}