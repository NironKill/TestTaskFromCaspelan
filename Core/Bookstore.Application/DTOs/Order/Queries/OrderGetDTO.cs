namespace Bookstore.Application.DTOs.Order.Queries
{
    public class OrderGetDTO
    {
        public Guid Id { get; set; }
        public long Number { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime OrderedIn { get; set; }
    }
}
