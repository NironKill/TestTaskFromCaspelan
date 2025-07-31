using MediatR;

namespace Bookstore.Application.Handlers.Orders.Commands.Create
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public HashSet<Guid> OrderIds { get; set; }
    }
}
