using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccess;

namespace EVABookShop.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly BookShopContext _context;
        protected DbSet<TEntity> _dbSet;

        public GenericRepository(BookShopContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetByIds(Expression<Func<TEntity, bool>> wherePredicate, int[] ids, string columnName)
        {
            return await _dbSet
                .Where(x => ids.Contains(EF.Property<int>(x, columnName))).Where(wherePredicate)
                .ToListAsync();
        }
        public async Task<List<TEntity>> GetByIds(Expression<Func<TEntity, bool>> wherePredicate, int[] ids)
        {
            var idName = _context.Model.FindEntityType(typeof(TEntity))
                .FindPrimaryKey().Properties.Single().Name;
            return await _dbSet
                .Where(x => ids.Contains(EF.Property<int>(x, idName))).Where(wherePredicate)
                .ToListAsync();
        }
        public async Task<List<TEntity>> GetByIds(Expression<Func<TEntity, bool>> wherePredicate, int[] ids, string columnName, List<string> include)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                query = query.Include(item);
            return await query
                .Where(x => ids.Contains(EF.Property<int>(x, columnName))).Where(wherePredicate)
                .ToListAsync();
        }
        public async Task<List<TEntity>> GetByIds(Expression<Func<TEntity, bool>> wherePredicate, int[] ids, List<string> include)
        {
            var idName = _context.Model.FindEntityType(typeof(TEntity))
                .FindPrimaryKey().Properties.Single().Name;
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                query = query.Include(item);
            return await query
                .Where(x => ids.Contains(EF.Property<int>(x, idName))).Where(wherePredicate)
                .ToListAsync();
        }
        public TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }
        public async Task AddRange(IEnumerable<TEntity> entityList)
        {
            await _dbSet.AddRangeAsync(entityList);
        }
        public TEntity Delete(int id)
        {
            var entity = GetById(id);
            return _dbSet.Remove(entity).Entity;
        }
        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<TEntity> Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateRange(IEnumerable<TEntity> entity)
        {
            _dbSet.UpdateRange(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<TEntity> FirstOrDefault()
        {
            return await _dbSet.FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<object>> GetData(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> selectPredicate, List<string> include)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                query = query.Include(item);
            return await query.Where(wherePredicate).Select(selectPredicate).ToListAsync();
        }
        public bool ExistsById(int id)
        {
            return _dbSet.Any(e => e == GetById(id));
        }
        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).SingleOrDefaultAsync();
        }
        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> GetData(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        public async Task<IQueryable<TEntity>> GetQueryableData(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }
        public bool ExistsByName(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }
        public TEntity GetById(int id, List<string> include)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                query = query.Include(item);
            return query.FirstOrDefault(e => EF.Property<int>(e, "Id") == id);
        }
        public async Task<IEnumerable<TEntity>> GetData(Expression<Func<TEntity, bool>> predicate, List<string> include)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                query = query.Include(item);
            return await query.Where(predicate).ToListAsync();
        }
        public async Task<IQueryable<TEntity>> GetQueryableData(Expression<Func<TEntity, bool>> predicate, List<string> include)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                query = query.Include(item);
            return query.Where(predicate).AsQueryable();
        }
        public async Task<IEnumerable<TEntity>> GroupBy(Expression<Func<TEntity, TEntity>> predicate, List<string> include)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                query = query.Include(item);
            return await query.GroupBy(predicate).SelectMany(g => g).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GroupBy(Expression<Func<TEntity, TEntity>> predicate)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            return await query.GroupBy(predicate).SelectMany(g => g).ToListAsync();
        }
        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate, List<string> include)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                query = query.Include(item);
            return await query.Where(predicate).SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAll(List<string> include)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                query = query.Include(item);
            return await query.ToListAsync();
        }
        public async Task RemoveRange(IEnumerable<TEntity> myObject)
        {
            _dbSet.RemoveRange(myObject);
            await _context.SaveChangesAsync();
        }
        public async Task<object> FirstOrDefault(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> selectPredicate, List<string> include)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                query = query.Include(item);
            return await query.Where(wherePredicate).Select(selectPredicate).FirstOrDefaultAsync();
        }
        public async Task<object> SingleOrDefault(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> selectPredicate, List<string> include)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                query = query.Include(item);
            return await query.Where(wherePredicate).Select(selectPredicate).SingleOrDefaultAsync();
        }
        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true)
        {
            return disableTracking
                ? _context.Set<TEntity>().Where(predicate).AsNoTracking()
                : _context.Set<TEntity>().Where(predicate);
        }
    }
} 