using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EVABookShop.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        TEntity GetById(int id);
        Task<List<TEntity>> GetByIds(Expression<Func<TEntity, bool>> wherePredicate, int[] ids);
        Task<List<TEntity>> GetByIds(Expression<Func<TEntity, bool>> wherePredicate, int[] ids, List<string> include);
        Task<List<TEntity>> GetByIds(Expression<Func<TEntity, bool>> wherePredicate, int[] ids, string columnName);
        Task<List<TEntity>> GetByIds(Expression<Func<TEntity, bool>> wherePredicate, int[] ids, string columnName, List<string> include);
        TEntity Add(TEntity myObject);
        Task AddRange(IEnumerable<TEntity> entityList);
        Task<TEntity> Update(TEntity entity);
        Task UpdateRange(IEnumerable<TEntity> entity);
        TEntity Delete(int id);
        Task<IEnumerable<TEntity>> GetData(Expression<Func<TEntity, bool>> predicate);
        Task<IQueryable<TEntity>> GetQueryableData(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefault();
        bool ExistsById(int id);
        bool ExistsByName(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(int id, List<string> include);
        Task<IEnumerable<TEntity>> GroupBy(Expression<Func<TEntity, TEntity>> predicate, List<string> include);
        Task<IEnumerable<TEntity>> GroupBy(Expression<Func<TEntity, TEntity>> predicate);
        Task<IEnumerable<TEntity>> GetData(Expression<Func<TEntity, bool>> predicate, List<string> include);
        Task<IEnumerable<object>> GetData(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> selectIncludePredicate, List<string> include);
        Task<IQueryable<TEntity>> GetQueryableData(Expression<Func<TEntity, bool>> predicate, List<string> include);
        Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate, List<string> include);
        Task<object> FirstOrDefault(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> selectPredicate, List<string> include);
        Task<object> SingleOrDefault(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> selectPredicate, List<string> include);
        Task<IEnumerable<TEntity>> GetAll(List<string> include);
        Task RemoveRange(IEnumerable<TEntity> myObject);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true);
    }
} 