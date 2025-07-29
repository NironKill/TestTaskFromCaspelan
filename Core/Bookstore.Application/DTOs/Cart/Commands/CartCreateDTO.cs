namespace Bookstore.Application.DTOs.Cart.Commands
{
    public class CartCreateDTO
    {
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }
    }
}
