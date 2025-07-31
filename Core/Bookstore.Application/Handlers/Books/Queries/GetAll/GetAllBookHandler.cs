using Bookstore.Application.Repositories.Interfaces;
using Bookstore.Application.Responses;
using MediatR;

namespace Bookstore.Application.Handlers.Books.Queries.GetAll
{
    public class GetAllBookHandler : IRequestHandler<GetAllBookQuery, ICollection<BookResponse>>
    {
        private readonly IBookRepository _book;

        public GetAllBookHandler(IBookRepository book) => _book = book;

        public async Task<ICollection<BookResponse>> Handle(GetAllBookQuery request, CancellationToken cancellationToken) =>
            await _book.GetAllByFilter(request, entity => new BookResponse
            {
                Id = entity.Id,
                Author = entity.Author,
                ISBN = entity.ISBN,
                Price = entity.Price,
                Publisher = entity.Publisher,
                PublishedIn = entity.PublishedIn.ToString("g"),
                Title = entity.Title,
                ImageUrl = entity.ImageUrl,
            }, cancellationToken);
    }
}
