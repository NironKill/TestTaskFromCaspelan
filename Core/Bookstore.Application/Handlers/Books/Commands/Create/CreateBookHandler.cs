using Bookstore.Application.Repositories.Interfaces;
using Bookstore.Domain.Entity;
using MediatR;

namespace Bookstore.Application.Handlers.Books.Commands.Create
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, Guid>
    {
        private readonly IBookRepository _book;

        public CreateBookHandler(IBookRepository book) => _book = book;

        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken) =>
            await _book.Create(request, request => new Book
            {
                Id = Guid.NewGuid(),
                Author = request.Author,
                ISBN = request.ISBN,
                ImageUrl = request.ImageUrl,
                Price = request.Price,
                Publisher = request.Publisher,
                Title = request.Title,
                PublishedIn = DateTime.UtcNow,
            }, cancellationToken);
    }
}
