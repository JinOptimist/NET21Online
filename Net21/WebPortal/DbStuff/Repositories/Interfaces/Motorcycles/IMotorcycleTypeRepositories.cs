using WebPortal.DbStuff.Models.Motorcycles;

namespace WebPortal.DbStuff.Repositories.Interfaces.Motorcycles
{
    public interface IMotorcycleTypeRepositories : IBaseRepository<MotorcycleType>
    {
        bool IsUniqType(string? type);
    }
}