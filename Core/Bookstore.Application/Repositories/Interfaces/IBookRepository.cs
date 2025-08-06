using Bookstore.Application.DTOs.Book;
using Bookstore.Application.Handlers.Books.Commands.Create;
using Bookstore.Application.Handlers.Books.Queries.GetAll;
using Bookstore.Application.Repositories.Abstract;
using Bookstore.Application.Responses;
using Bookstore.Domain.Entity;
using System.Linq.Expressions;

namespace Bookstore.Application.Repositories.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book, CreateBookCommand, BookResponse>
    {
        Task<ICollection<BookGetDTO>> GetAllByOrderId(Expression<Func<BookOrder, bool>> predicate, CancellationToken cancellationToken);
        Task<ICollection<BookResponse>> GetAllByFilter(GetAllBookQuery query, Func<Book, BookResponse> map, CancellationToken cancellationToken);
    }
}
