using Bookstore.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Domain.Entity
{
    public class Order : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Number { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime OrderedIn { get; set; }

        public ICollection<Book> Books { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
