using Bookstore.Domain.Base;

namespace Bookstore.Domain.Entity
{
    public class Book : BaseEntity
    {
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishedIn { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
