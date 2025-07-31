using Bookstore.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.Persistence.EntityTypeConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(o => o.Books).WithMany(b => b.Orders).UsingEntity<BookOrder>(
                x => x.HasOne<Book>(c => c.Book).WithMany(b => b.BookOrders).HasForeignKey(c => c.BookId),
                x => x.HasOne<Order>(c => c.Order).WithMany(o => o.BookOrders).HasForeignKey(c => c.OrderId));
        }
    }
}
