using Bookstore.Application.Handlers.Books.Commands.Create;
using Bookstore.Application.Repositories.Abstract;
using Bookstore.Application.Responses;
using Bookstore.Domain.Entity;

namespace Bookstore.Application.Repositories.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book, CreateBookCommand, BookResponse>
    {
        Task<ICollection<BookResponse>> GetAllByBookOrder(Guid orderId, CancellationToken cancellationToken);
    }
}
