using Bookstore.Application.Repositories.Interfaces;
using Bookstore.Application.Responses;
using MediatR;

namespace Bookstore.Application.Handlers.Orders.Queries.GetAll
{
    public class GetAllOrderHandler : IRequestHandler<GetAllOrderQuery, ICollection<OrderResponse>>
    {
        private readonly IOrderRepository _order;
        private readonly IBookRepository _book;

        public GetAllOrderHandler(IOrderRepository order, IBookRepository book)
        {
            _order = order;
            _book = book;
        }

        public async Task<ICollection<OrderResponse>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken) =>
            await _order.GetAllByFilter(request, entity => new OrderResponse
            {
                Id = entity.Id,
                Number = entity.Number,
                OrderedIn = entity.OrderedIn.ToString("g"),
                TotalCost = entity.TotalCost,
                Books = _book.GetAllByBookOrder(entity.Id, cancellationToken).Result,
            }, cancellationToken);
    }
}
