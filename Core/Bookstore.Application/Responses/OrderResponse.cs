namespace Bookstore.Application.Responses
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public long Number { get; set; }
        public decimal TotalCost { get; set; }
        public string OrderedIn { get; set; }

        public ICollection<BookResponse> Books { get; set; }
    }
}
