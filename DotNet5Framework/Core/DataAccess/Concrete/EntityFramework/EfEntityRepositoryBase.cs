using Core.DataAccess.Abstract;
using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Core.DataAccess.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {


        #region Normal Methods
        public TEntity Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                return entity;
            }
        }

        public IEnumerable<TEntity> AddRange(IList<TEntity> entityList)
        {
            using (TContext context = new TContext())
            {
                context.Set<TEntity>().AddRange(entityList);
                context.SaveChanges();
                return entityList;
            }
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().Any(predicate);
            }
        }

        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            using (TContext context = new TContext())
            {
                return (predicate == null ? context.Set<TEntity>().Count() : context.Set<TEntity>().Count(predicate));
            }
        }

        public TEntity Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
                return entity;
            }
        }

        public IEnumerable<TEntity> DeleteRange(IList<TEntity> entityList)
        {
            using (TContext context = new TContext())
            {
                context.Set<TEntity>().RemoveRange(entityList);
                context.SaveChanges();
                return entityList;
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }
                return query.FirstOrDefault();
            }
        }

        public TEntity GetInOrderByDescending(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>> orderByPredicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (orderByPredicate != null)
                    query.OrderByDescending(orderByPredicate);
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }
                return query.FirstOrDefault();
            }
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }
                return query.ToList();
            }
        }

        public IEnumerable<TEntity> GetListInOrderByDescending(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, dynamic>> orderByPredicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (orderByPredicate != null)
                    query.OrderByDescending(orderByPredicate);
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }
                return query.ToList();
            }
        }

        public IEnumerable<TEntity> GetListPaginated(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }
                query.Skip((pageNumber * pageSize) - pageSize).Take(pageSize);
                return query.ToList();
            }
        }

        public IEnumerable<TEntity> GetListPaginatedInOrderByDescending(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, dynamic>> orderByPredicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (orderByPredicate != null)
                    query.OrderByDescending(orderByPredicate);
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }
                query.Skip((pageNumber * pageSize) - pageSize).Take(pageSize);
                return query.ToList();
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
                return entity;
            }
        }

        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entityList)
        {
            using (TContext context = new TContext())
            {
                foreach (var entity in entityList)
                {
                    var updatedEntity = context.Entry(entity);
                    updatedEntity.State = EntityState.Modified;
                }
                context.SaveChanges();
                return entityList;
            }
        }
        #endregion

        #region Async Methods
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                await Task.Run(() => { context.Entry<TEntity>(entity).State = EntityState.Added; });
                await context.SaveChangesAsync();
                return entity;
            }
        }
        public async Task<IEnumerable<TEntity>> AddRangeAsync(IList<TEntity> entityList)
        {
            using (TContext context = new TContext())
            {
                await context.Set<TEntity>().AddRangeAsync(entityList);
                await context.SaveChangesAsync();
                return entityList;
            }
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                await Task.Run(() => { context.Entry<TEntity>(entity).State = EntityState.Deleted; });
                await context.SaveChangesAsync();
                return entity;
            }
        }
        public async Task<IEnumerable<TEntity>> DeleteRangeAsync(IList<TEntity> entityList)
        {
            using (TContext context = new TContext())
            {
                await Task.Run(() => { context.Set<TEntity>().RemoveRange(entityList); });
                await context.SaveChangesAsync();
                return entityList;
            }
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }
                return await query.FirstOrDefaultAsync();
            }
        }
        public async Task<TEntity> GetInOrderByDescendingAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>> orderByPredicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (orderByPredicate != null)
                    query.OrderByDescending(orderByPredicate);
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }
                return await query.FirstOrDefaultAsync();
            }
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }
                return await query.ToListAsync();
            }
        }

        public async Task<IEnumerable<TEntity>> GetListInOrderByDescendingAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, dynamic>> orderByPredicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (orderByPredicate != null)
                    query.OrderByDescending(orderByPredicate);
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }
                return await query.ToListAsync();
            }
        }

        public async Task<IEnumerable<TEntity>> GetListPaginatedAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }
                query.Skip((pageNumber * pageSize) - pageSize).Take(pageSize);
                return await query.ToListAsync();
            }
        }

        public async Task<IEnumerable<TEntity>> GetListPaginatedInOrderByDescendingAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, dynamic>> orderByPredicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (orderByPredicate != null)
                    query.OrderByDescending(orderByPredicate);
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }
                query.Skip((pageNumber * pageSize) - pageSize).Take(pageSize);
                return await query.ToListAsync();
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {

            using (TContext context = new TContext())
            {
                await Task.Run(() => { context.Entry<TEntity>(entity).State = EntityState.Modified; });
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entityList)
        {
            using (TContext context = new TContext())
            {
                await Task.Run(() =>
                {
                    foreach (var entity in entityList)
                    {
                        var updatedEntity = context.Entry(entity);
                        updatedEntity.State = EntityState.Modified;
                    }
                });
                await context.SaveChangesAsync();
                return entityList;
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            using (TContext context = new TContext())
            {
                return await context.Set<TEntity>().AnyAsync(predicate);
            }
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            using (TContext context = new TContext())
            {
                return (predicate == null ? await context.Set<TEntity>().CountAsync() : await context.Set<TEntity>().CountAsync(predicate));
            }
        }
        #endregion
    }
}
