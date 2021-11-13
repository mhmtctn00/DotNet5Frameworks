using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Abstract
{
    public interface IEntityRepository<T>
    {
        #region Normal Methods
        T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetListPaginated(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetListPaginatedInOrderByDescending(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null, Expression<Func<T, dynamic>> orderByPredicate = null, params Expression<Func<T, object>>[] includeProperties);
        T Add(T entity);
        IEnumerable<T> AddRange(IList<T> entityList);
        T Update(T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> entityList);
        T Delete(T entity);
        IEnumerable<T> DeleteRange(IList<T> entityList);
        bool Any(Expression<Func<T, bool>> predicate = null);
        int Count(Expression<Func<T, bool>> predicate = null);
        #endregion

        #region Async Methods
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetListPaginatedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetListPaginatedInOrderByDescendingAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null, Expression<Func<T, dynamic>> orderByPredicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IList<T> entityList);
        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entityList);
        Task<T> DeleteAsync(T entity);
        Task<IEnumerable<T>> DeleteRangeAsync(IList<T> entityList);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        #endregion
    }
}
