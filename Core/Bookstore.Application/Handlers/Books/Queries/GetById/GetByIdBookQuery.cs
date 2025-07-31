using Bookstore.Application.Responses;
using MediatR;

namespace Bookstore.Application.Handlers.Books.Queries.GetById
{
    public class GetByIdBookQuery : IRequest<BookResponse>
    {
        public Guid Id { get; set; }
    }
}
