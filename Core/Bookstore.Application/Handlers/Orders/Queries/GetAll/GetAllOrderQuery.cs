using Bookstore.Application.Responses;
using MediatR;

namespace Bookstore.Application.Handlers.Orders.Queries.GetAll
{
    public class GetAllOrderQuery : IRequest<ICollection<OrderResponse>>
    {
        public DateTime? OrderDateFrom { get; set; }
        public DateTime? OrderDateTo { get; set; }
        public int? OrderNumberFrom { get; set; }
        public int? OrderNumberTo { get; set; }
    }
}
