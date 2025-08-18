using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories.Notes
{
    public abstract class BaseDbRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : BaseModel
    {
        protected NotesDbContext _portalContext;
        protected DbSet<TDbModel> _dbSet;

        public BaseDbRepository(NotesDbContext portalContext)
        {
            _portalContext = portalContext;
            _dbSet = portalContext.Set<TDbModel>();
        }

        public List<TDbModel> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Remove(int id)
        {
            var user = _dbSet.First(x => x.Id == id);
            Remove(user);
        }

        public void Remove(TDbModel model)
        {
            _dbSet.Remove(model);
            _portalContext.SaveChanges();
        }

        public TDbModel Add(TDbModel model)
        {
            _dbSet.Add(model);
            _portalContext.SaveChanges();
            return model;
        }
    }
}