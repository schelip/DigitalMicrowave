using DigitalMicrowave.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DigitalMicrowave.Business.Repositories
{
    public class BaseRepository<TId, TEntity> where TEntity : class
    {
        private DigitalMicrowaveContext _context;
        private DbSet<TEntity> _dbSet;

        public BaseRepository(DigitalMicrowaveContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return orderBy != null ?
                await orderBy(query).ToListAsync() :
                await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(TId id) => await _dbSet.FindAsync(id);

        public virtual async Task Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public virtual async Task Delete(TId id)
        {
            TEntity entity = _dbSet.Find(id);
            await Delete(entity);
        }

        public virtual async Task Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}