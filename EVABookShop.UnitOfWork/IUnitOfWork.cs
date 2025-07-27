using System;
using System.Data;
using System.Threading.Tasks;
using EVABookShop.Repositories;

namespace EVABookShop.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChanges();
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task BeginTransactionAsync(IsolationLevel isolationLevel);
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
} 