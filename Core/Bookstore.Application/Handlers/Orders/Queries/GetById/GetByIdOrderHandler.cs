using Bookstore.Application.Repositories.Interfaces;
using Bookstore.Application.Responses;
using MediatR;

namespace Bookstore.Application.Handlers.Orders.Queries.GetById
{
    public class GetByIdOrderHandler : IRequestHandler<GetByIdOrderQuery, OrderResponse>
    {
        private readonly IOrderRepository _order;
        private readonly IBookRepository _book;

        public GetByIdOrderHandler(IOrderRepository order, IBookRepository book)
        {
            _order = order;
            _book = book;
        }

        public async Task<OrderResponse> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken) =>
            await _order.Get(x => x.Id == request.Id, entity => new OrderResponse
            {
                Id = entity.Id,
                Number = entity.Number,
                OrderedIn = entity.OrderedIn.ToString("g"),
                TotalCost = entity.TotalCost,
                Books = _book.GetAllByBookOrder(request.Id, cancellationToken).Result,
            }, cancellationToken);
    }
}
