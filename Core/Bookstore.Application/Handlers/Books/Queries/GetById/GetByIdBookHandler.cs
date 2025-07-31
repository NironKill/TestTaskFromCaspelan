using Bookstore.Application.Repositories.Interfaces;
using Bookstore.Application.Responses;
using MediatR;

namespace Bookstore.Application.Handlers.Books.Queries.GetById
{
    public class GetByIdBookHandler : IRequestHandler<GetByIdBookQuery, BookResponse>
    {
        private readonly IBookRepository _book;

        public GetByIdBookHandler(IBookRepository book) => _book = book;

        public async Task<BookResponse> Handle(GetByIdBookQuery request, CancellationToken cancellationToken) =>
            await _book.Get(x => x.Id == request.Id, entity => new BookResponse
            {
                Id = entity.Id,
                Author = entity.Author,
                Title = entity.Title,
                ISBN = entity.ISBN,
                Price = entity.Price,
                Publisher = entity.Publisher,
                PublishedIn = entity.PublishedIn.ToString("g"),
                ImageUrl = entity.ImageUrl,
            }, cancellationToken);
    }
}
