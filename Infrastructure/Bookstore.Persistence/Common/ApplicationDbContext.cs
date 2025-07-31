using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Persistence.Common
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BookOrder> BookOrders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
