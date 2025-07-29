using Bookstore.Domain.Base;

namespace Bookstore.Domain.Entity
{
    public class Cart : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }

        public Order Order { get; set; }
        public Book Book { get; set; }
    }
}
