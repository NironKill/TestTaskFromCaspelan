using Bookstore.Application.DTOs.Book;
using Bookstore.Application.DTOs.Order;
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

        public async Task<ICollection<OrderResponse>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            ICollection<OrderGetDTO> orders = await _order.GetAllByFilter(request, entity => new OrderGetDTO
            {
                Id = entity.Id,
                Number = entity.Number,
                OrderedIn = entity.OrderedIn.ToString("g"),
                TotalCost = entity.TotalCost
            }, cancellationToken);

            HashSet<Guid> orderIds = orders.Select(x => x.Id).ToHashSet();
            ICollection<BookGetDTO> books = await _book.GetAllByOrderId(x => orderIds.Contains(x.OrderId), cancellationToken);

            return orders.Select(o => new OrderResponse
            {
                Id = o.Id,
                Number = o.Number,
                OrderedIn = o.OrderedIn,
                TotalCost = o.TotalCost,
                Books = books.Where(x => x.OrderId == o.Id).Select(x => new BookResponse
                {
                    Id = x.Id,
                    ISBN = x.ISBN,
                    Title = x.Title,
                    Author = x.Author,
                    Price = x.Price,
                    Publisher = x.Publisher,
                    PublishedIn = x.PublishedIn,
                    ImageUrl = x.ImageUrl,
                }).ToList()
            }).ToList();           
        }
    }
}
