using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ForMemory.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Formemory.Repository
{
    public abstract class BaseRepository<T>
    where T : class, IEntity
    {
        protected MyDbContext _dbContext;
        protected DbSet<T> _entities;
        protected BaseRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<T>();
        }

        public virtual void Insert(T entity)
        {
            _dbContext.Add(entity);
        }

        public virtual void Insert(IEnumerable<T> entities)
        {
            _dbContext.AddRange(entities);
        }

        public virtual T QueryById(Guid id)
        {
            return _entities.FirstOrDefault(t => t.Id == id);
        }

        public virtual int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public virtual async Task<int> CommitAsync(CancellationToken cancellation = default)
        {
            return await _dbContext.SaveChangesAsync(cancellation);
        }

        public virtual int TransactionCommit()
        {
            using var tran = _dbContext.Database.BeginTransaction();
            try
            {
                var result = Commit();
                tran.Commit();
                return result;
            }
            catch (Exception)
            {
                tran.Rollback();
                return -1;
            }
        }

        public virtual async Task<int> TransactionCommitAsync(CancellationToken cancellation = default)
        {
            await using var tran = await _dbContext.Database.BeginTransactionAsync(cancellation);
            try
            {
                var result = await CommitAsync(cancellation);
                tran.Commit();
                return result;
            }
            catch (Exception)
            {
                tran.Rollback();
                return -1;
            }
        }
    }
}