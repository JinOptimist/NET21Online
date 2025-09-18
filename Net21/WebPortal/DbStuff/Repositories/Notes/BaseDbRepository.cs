using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;

namespace WebPortal.DbStuff.Repositories.Notes
{
    public abstract class BaseDbRepository<TDbModel> : IBaseDbRepository<TDbModel> where TDbModel : BaseModel
    {
        protected NotesDbContext _portalContext;
        protected DbSet<TDbModel> _dbSet;

        public BaseDbRepository(NotesDbContext portalContext)
        {
            _portalContext = portalContext;
            _dbSet = portalContext.Set<TDbModel>();
        }

        public virtual TDbModel GetFirstById(int id)
        {
            return _dbSet.First(c => c.Id == id);
        }

        public virtual List<TDbModel> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual void Remove(int id)
        {
            var user = _dbSet.First(x => x.Id == id);
            Remove(user);
        }

        public virtual void Remove(TDbModel model)
        {
            _dbSet.Remove(model);
            _portalContext.SaveChanges();
        }

        public virtual void Update(TDbModel model)
        {
            _dbSet.Update(model);
            _portalContext.SaveChanges();
        }

        public virtual TDbModel Add(TDbModel model)
        {
            _dbSet.Add(model);
            _portalContext.SaveChanges();
            return model;
        }
    }
}