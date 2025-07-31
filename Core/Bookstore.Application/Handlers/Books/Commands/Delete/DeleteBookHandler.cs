using Bookstore.Application.Repositories.Interfaces;
using MediatR;

namespace Bookstore.Application.Handlers.Books.Commands.Delete
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IBookRepository _book;

        public DeleteBookHandler(IBookRepository book) => _book = book;

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken) =>
            await _book.Delete(x => x.Id == request.Id, cancellationToken);
    }
}
