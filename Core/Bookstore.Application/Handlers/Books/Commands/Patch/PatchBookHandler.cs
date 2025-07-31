using Bookstore.Application.Repositories.Interfaces;
using Bookstore.Application.Responses;
using MediatR;

namespace Bookstore.Application.Handlers.Books.Commands.Patch
{
    public class PatchBookHandler : IRequestHandler<PatchBookCommand, BookResponse>
    {
        private readonly IBookRepository _book;

        public PatchBookHandler(IBookRepository book) => _book = book;

        public async Task<BookResponse> Handle(PatchBookCommand request, CancellationToken cancellationToken) =>
            await _book.Update(x => x.Id == request.Id, (entity) =>
            {
                entity.Price = request.Price;
            }, map => new BookResponse
            {
                Id = map.Id,
                Author = map.Author,
                ImageUrl = map.ImageUrl,
                ISBN = map.ISBN,
                Price = map.Price,
                PublishedIn = map.PublishedIn.ToString("g"),
                Publisher = map.Publisher,
                Title = map.Title
            }, cancellationToken);
    }
}
