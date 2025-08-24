using WebPortal.DbStuff.Models.Motorcycles;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface IMotorcycleTypeRepositories : IBaseRepository<MotorcycleType>
    {
        bool IsUniqType(string? type);
    }
}