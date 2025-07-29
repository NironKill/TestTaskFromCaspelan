using Bookstore.Application.DTOs.Book.Queries;

namespace Bookstore.Application.DTOs.Cart.Queries
{
    public class CartGetDTO
    {
        public Guid Id { get; set; }
        public long OrderNumber { get; set; }
        public List<BookGetDTO> Books { get; set; }
    }
}
