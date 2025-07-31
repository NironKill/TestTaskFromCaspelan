using Bookstore.Application.Responses;
using MediatR;

namespace Bookstore.Application.Handlers.Books.Queries.GetAll
{
    public class GetAllBookQuery : IRequest<ICollection<BookResponse>>
    {
    }
}
