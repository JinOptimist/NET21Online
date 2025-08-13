using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Repositories
{
    public abstract class BaseRepository<DbModel> : IBaseRepository<DbModel> where DbModel : BaseModel
    {
        protected WebPortalContext _portalContext;
        protected DbSet<DbModel> _dbSet;

        public BaseRepository(WebPortalContext portalContext)
        {
            _portalContext = portalContext;
            _dbSet = portalContext.Set<DbModel>();
        }

        public List<DbModel> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Remove(int id)
        {
            var user = _dbSet.First(x => x.Id == id);
            Remove(user);
        }

        public void Remove(DbModel model)
        {
            _dbSet.Remove(model);
            _portalContext.SaveChanges();
        }

        public DbModel Add(DbModel model)
        {
            _dbSet.Add(model);
            _portalContext.SaveChanges();
            return model;
        }
    }
}
