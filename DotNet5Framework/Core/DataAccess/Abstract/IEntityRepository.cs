using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.DataAccess.Abstract
{
    public interface IEntityRepository<T>
    {
        #region Normal Methods
        #region Get Methods
        T Get(Expression<Func<T, bool>> predicate);
        T Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
        T Get(Expression<Func<T, bool>> predicate, params string[] includeProperties);
        T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, params string[] includeProperties);
        T Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, params Expression<Func<T, object>>[] includeProperties);
        #endregion
        #region GetList Methods
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicate = null, params string[] includeProperties);
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params string[] includeProperties);
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetListPaginated(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        IEnumerable<T> GetListPaginated(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null, params string[] includeProperties);
        IEnumerable<T> GetListPaginated(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetListPaginated(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params string[] includeProperties);
        IEnumerable<T> GetListPaginated(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includeProperties);
        #endregion
        #region Add Methods
        T Add(T entity);
        IEnumerable<T> AddRange(IList<T> entityList);
        #endregion
        #region Update Methods
        T Update(T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> entityList);
        #endregion
        #region Delete Methods
        T Delete(T entity);
        IEnumerable<T> DeleteRange(IList<T> entityList);
        #endregion
        #region Other Methods
        bool Any(Expression<Func<T, bool>> predicate = null);
        int Count(Expression<Func<T, bool>> predicate = null);
        int SaveChanges();
        #endregion
        #endregion

        #region Async Methods
        #region Get Methods
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, params string[] includeProperties);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, params Expression<Func<T, object>>[] includeProperties);
        #endregion
        #region GetList Methods
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, params string[] includeProperties);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params string[] includeProperties);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetListPaginatedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<IEnumerable<T>> GetListPaginatedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetListPaginatedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null, params string[] includeProperties);
        Task<IEnumerable<T>> GetListPaginatedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetListPaginatedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params string[] includeProperties);
        #endregion
        #region Add Methods
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IList<T> entityList);
        #endregion
        #region Update Methods
        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entityList);
        #endregion
        #region Delete Methods
        Task<T> DeleteAsync(T entity);
        Task<IEnumerable<T>> DeleteRangeAsync(IList<T> entityList);
        #endregion
        #region Other Methods
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        Task<int> SaveChangesAsync();
        #endregion
        #endregion

        IQueryable<T> Query();
        TResult InTransaction<TResult>(Func<TResult> action, Action successAction = null, Action<Exception> exceptionAction = null);
    }
}
