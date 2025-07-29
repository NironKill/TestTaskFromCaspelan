using Bookstore.Application.DTOs.Book.Commands;
using Bookstore.Application.DTOs.Book.Queries;
using Bookstore.Application.Repositories.Abstract;
using Bookstore.Domain.Entity;

namespace Bookstore.Application.Repositories.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book, BookCreateDTO, BookGetDTO>
    {
    }
}
