using DevicesMinimalApi.DbStuff;
using DevicesMinimalApi.DbStuff.Model;
using Microsoft.EntityFrameworkCore;

namespace WebPortal.DbStuff.Repositories
{
    public abstract class BaseRepository<DbModel>
         where DbModel : BaseDBModel
    {
        protected readonly DeviceDbContext  _dbContext;
        protected readonly DbSet<DbModel> _dbSet;

        public BaseRepository(DeviceDbContext portalContext)
        {
            _dbContext = portalContext;
            _dbSet = portalContext.Set<DbModel>();
        }

        public virtual List<DbModel> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual void Remove(int id)
        {
            var user = _dbSet.First(x => x.Id == id);
            Remove(user);
        }

        public virtual void Remove(DbModel model)
        {
            _dbSet.Remove(model);
            _dbContext.SaveChanges();
        }

        public virtual void RemoveAll(List<DbModel> models)
        {
            _dbSet.RemoveRange(models);
            _dbContext.SaveChanges();
        }

        public virtual DbModel Add(DbModel model)
        {
            _dbSet.Add(model);
            _dbContext.SaveChanges();
            return model;
        }

        public virtual List<DbModel> AddRange(List<DbModel> models)
        {
            _dbSet.AddRange(models);
            _dbContext.SaveChanges();
            return models;
        }

        public virtual DbModel GetFirstById(int id)
        {
            return _dbSet.First(c => c.Id == id);
        }

        public void Update(DbModel model)
        {
            _dbSet.Update(model);
            _dbContext.SaveChanges();
        }

        public bool Any()
        {
            return _dbSet.Any();
        }
    }
}
