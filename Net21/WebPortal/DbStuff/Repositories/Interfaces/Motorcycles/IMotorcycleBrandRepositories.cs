using WebPortal.DbStuff.Models.Motorcycles;

namespace WebPortal.DbStuff.Repositories.Interfaces.Motorcycles
{
    public interface IMotorcycleBrandRepositories : IBaseRepository<Brand>
    {
        bool IsUniqBrand(string? type);
    }
}