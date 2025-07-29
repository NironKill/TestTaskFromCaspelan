namespace Bookstore.Application.DTOs.Book.Queries
{
    public class BookGetDTO
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishedIn { get; set; }
        public string ImageUrl { get; set; }
    }
}
