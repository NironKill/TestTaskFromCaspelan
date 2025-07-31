using MediatR;

namespace Bookstore.Application.Handlers.Orders.Commands.Delete
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
