using Bookstore.Application.Responses;
using MediatR;

namespace Bookstore.Application.Handlers.Books.Commands.Patch
{
    public class PatchBookCommand : IRequest<BookResponse>
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
    }
}
