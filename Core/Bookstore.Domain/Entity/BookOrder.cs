using Bookstore.Domain.Base;

namespace Bookstore.Domain.Entity
{
    public class BookOrder : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }
        public decimal Price { get; set; }

        public Order Order { get; set; }
        public Book Book { get; set; }
    }
}
