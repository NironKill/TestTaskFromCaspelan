using MediatR;

namespace Bookstore.Application.Handlers.Books.Commands.Delete
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
