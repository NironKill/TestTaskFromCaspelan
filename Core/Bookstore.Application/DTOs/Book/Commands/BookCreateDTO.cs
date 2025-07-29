namespace Bookstore.Application.DTOs.Book.Commands
{
    public class BookCreateDTO
    {
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; } 
        public string Author { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
