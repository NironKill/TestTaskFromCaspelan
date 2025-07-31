using Bookstore.Application.Responses;
using MediatR;

namespace Bookstore.Application.Handlers.Orders.Queries.GetById
{
    public class GetByIdOrderQuery : IRequest<OrderResponse>
    {
        public Guid Id { get; set; }
    }
}
