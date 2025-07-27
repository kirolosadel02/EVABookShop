using Microsoft.EntityFrameworkCore;
using Models;
using DataAccess.Configurations;
using EVABookShop.Configurations;
namespace DataAccess
{
    public class BookShopContext : DbContext
    {
        public BookShopContext(DbContextOptions<BookShopContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
} 