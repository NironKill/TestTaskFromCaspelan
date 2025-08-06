using Bookstore.Application.DTOs.Book;
using Bookstore.Application.DTOs.Order;
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

        public async Task<OrderResponse> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken)
        {
            OrderGetDTO order = await _order.Get(x => x.Id == request.Id, entity => new OrderGetDTO
            {
                Id = entity.Id,
                Number = entity.Number,
                OrderedIn = entity.OrderedIn.ToString("g"),
                TotalCost = entity.TotalCost
            }, cancellationToken);

            ICollection<BookGetDTO> books = await _book.GetAllByOrderId(x => x.OrderId == order.Id, cancellationToken);

            return new OrderResponse()
            {
                Id = order.Id,
                Number = order.Number,
                OrderedIn = order.OrderedIn,
                TotalCost = order.TotalCost,
                Books = books.Where(x => x.OrderId == order.Id).Select(x => new BookResponse
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
            };          
        }
    }
}
