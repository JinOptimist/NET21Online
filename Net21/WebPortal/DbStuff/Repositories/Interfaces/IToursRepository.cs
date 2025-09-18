using WebPortal.DbStuff.DataModels;
using WebPortal.DbStuff.Models.Tourism;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface IToursRepository : IBaseRepository<Tours>
    {
        List<Tours> GetShopItemWithAuthor();
        bool IsUniqName(string? name);
        public List<ToursAutorStatiscticModel> GetTourAutor();
    }
}