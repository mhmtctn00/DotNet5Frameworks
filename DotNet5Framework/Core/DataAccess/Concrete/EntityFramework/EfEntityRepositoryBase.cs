﻿using Core.DataAccess.Abstract;
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
        public EfEntityRepositoryBase(TContext context)
        {
            Context = context;
        }

        protected TContext Context { get; }


        #region Normal Methods
        public TEntity Add(TEntity entity)
        {
            return Context.Add(entity).Entity;
        }

        public IEnumerable<TEntity> AddRange(IList<TEntity> entityList)
        {
            Context.AddRange(entityList);
            return entityList;
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Any(predicate); //?
        }

        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? Context.Set<TEntity>().Count() : Context.Set<TEntity>().Count(predicate);
        }

        public TEntity Delete(TEntity entity)
        {
            return Context.Remove(entity).Entity;
        }

        public IEnumerable<TEntity> DeleteRange(IList<TEntity> entityList)
        {
            Context.RemoveRange(entityList);
            return entityList;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
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

        public TEntity GetInOrderByDescending(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>> orderByPredicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            if (orderByPredicate != null)
                query = query.OrderByDescending(orderByPredicate);
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

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
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

        public IEnumerable<TEntity> GetListInOrderByDescending(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, dynamic>> orderByPredicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            if (orderByPredicate != null)
                query = query.OrderByDescending(orderByPredicate);
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

        public IEnumerable<TEntity> GetListPaginated(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
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
            query = query.Skip((pageNumber * pageSize) - pageSize).Take(pageSize);
            return query.ToList();
        }

        public IEnumerable<TEntity> GetListPaginatedInOrderByDescending(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, dynamic>> orderByPredicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            if (orderByPredicate != null)
                query = query.OrderByDescending(orderByPredicate);
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
            query = query.Skip((pageNumber * pageSize) - pageSize).Take(pageSize);
            return query.ToList();
        }

        public TEntity Update(TEntity entity)
        {
            return Context.Update(entity).Entity;
        }

        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entityList)
        {
            foreach (var entity in entityList)
            {
                Context.Update(entity);
            }
            return entityList;
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
        #endregion

        #region Async Methods
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await Task.Run(() => { Context.Add(entity); });
            return entity;
        }
        public async Task<IEnumerable<TEntity>> AddRangeAsync(IList<TEntity> entityList)
        {
            await Context.AddRangeAsync(entityList);
            return entityList;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            await Task.Run(() => { Context.Remove(entity); });
            return entity;
        }
        public async Task<IEnumerable<TEntity>> DeleteRangeAsync(IList<TEntity> entityList)
        {
            await Task.Run(() => { Context.RemoveRange(entityList); });
            return entityList;
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
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
        public async Task<TEntity> GetInOrderByDescendingAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>> orderByPredicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            if (orderByPredicate != null)
                query = query.OrderByDescending(orderByPredicate);
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

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
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

        public async Task<IEnumerable<TEntity>> GetListInOrderByDescendingAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, dynamic>> orderByPredicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            if (orderByPredicate != null)
                query = query.OrderByDescending(orderByPredicate);
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

        public async Task<IEnumerable<TEntity>> GetListPaginatedAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
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
            query = query.Skip((pageNumber * pageSize) - pageSize).Take(pageSize);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetListPaginatedInOrderByDescendingAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, dynamic>> orderByPredicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            if (orderByPredicate != null)
                query = query.OrderByDescending(orderByPredicate);
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
            query = query.Skip((pageNumber * pageSize) - pageSize).Take(pageSize);
            return await query.ToListAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {

            await Task.Run(() => { Context.Update(entity); });
            return entity;
        }

        public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entityList)
        {
            await Task.Run(() =>
            {
                foreach (var entity in entityList)
                {
                    Context.Update(entity);
                }
            });
            return entityList;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return (predicate is null ? await Context.Set<TEntity>().CountAsync() : await Context.Set<TEntity>().CountAsync(predicate));
        }

        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }
        #endregion

        public IQueryable<TEntity> Query()
        {
            return Context.Set<TEntity>();
        }
        public TResult InTransaction<TResult>(Func<TResult> action, Action successAction = null, Action<Exception> exceptionAction = null)
        {
            var result = default(TResult);
            try
            {
                if (Context.Database.ProviderName.EndsWith("InMemory"))
                {
                    result = action();
                    SaveChanges();
                }
                else
                {
                    using (var tx = Context.Database.BeginTransaction())
                    {
                        try
                        {
                            result = action();
                            SaveChanges();
                            tx.Commit();
                        }
                        catch (Exception)
                        {
                            tx.Rollback();
                            throw;
                        }
                    }
                }

                successAction?.Invoke();
            }
            catch (Exception ex)
            {
                if (exceptionAction == null)
                {
                    throw;
                }

                exceptionAction(ex);
            }

            return result;
        }
    }
}
