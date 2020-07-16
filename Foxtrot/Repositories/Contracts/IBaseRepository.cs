using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Foxtrot.Repositories.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        Task<T> GetById(object id);
        Task Insert(T entity);
        Task<T> Delete(object id);
        void Delete(T entity);
        Task Update(T entity);
        Task SaveChangesAsync();
    }
}