using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ForMemory.Entities.Interfaces;

namespace Formemory.Repository
{
    public abstract class BaseRepository<T>
    where T : class, IEntity
    {
        protected MyDbContext _dbContext;
        protected BaseRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual void Insert(T entity)
        {
            _dbContext.Add(entity);
        }

        public virtual void Insert(IEnumerable<T> entities)
        {
            _dbContext.AddRange(entities);
        }

        public virtual int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public virtual async Task<int> CommitAsync(CancellationToken cancellation = default)
        {
            return await _dbContext.SaveChangesAsync(cancellation);
        }
    }
}