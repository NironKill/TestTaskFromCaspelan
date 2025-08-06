namespace Bookstore.Application.DTOs.Order
{
    public class OrderGetDTO
    {
        public Guid Id { get; set; }
        public long Number { get; set; }
        public decimal TotalCost { get; set; }
        public string OrderedIn { get; set; }
    }
}
