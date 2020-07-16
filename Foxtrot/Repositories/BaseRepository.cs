using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Foxtrot.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Foxtrot.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly FoxtrotContext _context;
        private readonly DbSet<T> _dbSet;

        protected BaseRepository(FoxtrotContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;
            
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (string includeProperty in includeProperties.Split(
                new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual async Task<T> GetById(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task Insert(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task<T> Delete(object id)
        {
            T entity = await GetById(id);
            Delete(entity);
            await _context.SaveChangesAsync();
            
            return entity;
        }

        public virtual void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public virtual async Task Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}