using System;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using DataAccess;
using EVABookShop.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EVABookShop.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookShopContext _context;
        private Hashtable _repositories;

        public UnitOfWork(BookShopContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            _repositories ??= new Hashtable();
            string typeName = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(typeName))
            {
                Type repoType = typeof(GenericRepository<>);
                object repoInstance = Activator.CreateInstance(repoType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(typeName, repoInstance);
            }
            return (IGenericRepository<TEntity>)_repositories[typeName];
        }

        public async Task BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            await _context.Database.BeginTransactionAsync(isolationLevel);
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
} 