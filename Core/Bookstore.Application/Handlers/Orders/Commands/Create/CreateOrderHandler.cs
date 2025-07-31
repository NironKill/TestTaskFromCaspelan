using Bookstore.Application.DTOs.BookOrder;
using Bookstore.Application.Repositories.Interfaces;
using Bookstore.Application.Responses;
using Bookstore.Domain.Entity;
using MediatR;

namespace Bookstore.Application.Handlers.Orders.Commands.Create
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _order;
        private readonly IBookRepository _book;

        public CreateOrderHandler(IOrderRepository order, IBookRepository book)
        {
            _order = order;
            _book = book;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            ICollection<BookResponse> books = await _book.GetAll(x => request.BookIds.Contains(x.Id), entity => new BookResponse
            {
                Id = entity.Id,
                Author = entity.Author,
                ImageUrl = entity.ImageUrl,
                ISBN = entity.ISBN,
                Price = entity.Price,
                PublishedIn = entity.PublishedIn.ToString("g"),
                Publisher = entity.Publisher,
                Title = entity.Title,
            }, cancellationToken);
            
            Guid orderId = await _order.Create(request, request => new Order
            {
                Id = Guid.NewGuid(),
                OrderedIn = DateTime.UtcNow,
                TotalCost = books.Sum(x => x.Price)
            }, cancellationToken);

            await _order.AddBookOrder(books.Select(book => new CreateBookOrderDTO
            {
                OrderId = orderId,
                BookId = book.Id,
                Price = book.Price,
            }), cancellationToken);
            
            return orderId;
        }
    }
}
