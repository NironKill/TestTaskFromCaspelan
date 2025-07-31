using Bookstore.Application.Repositories.Interfaces;
using MediatR;

namespace Bookstore.Application.Handlers.Orders.Commands.Delete
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IOrderRepository _order;

        public DeleteOrderHandler(IOrderRepository order) => _order = order;

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken) =>
            await _order.Delete(x => x.Id == request.Id, cancellationToken);
    }
}
