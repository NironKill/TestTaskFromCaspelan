namespace Bookstore.Application.DTOs.BookOrder
{
    public class CreateBookOrderDTO
    {
        public Guid BookId { get; set; }
        public Guid OrderId { get; set; }
        public decimal Price { get; set; }
    }
}
