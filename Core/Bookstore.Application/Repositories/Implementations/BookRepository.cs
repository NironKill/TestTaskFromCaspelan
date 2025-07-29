using Bookstore.Application.DTOs.Book.Commands;
using Bookstore.Application.DTOs.Book.Queries;
using Bookstore.Application.Interfaces;
using Bookstore.Application.Repositories.Abstract;
using Bookstore.Application.Repositories.Interfaces;
using Bookstore.Domain.Entity;

namespace Bookstore.Application.Repositories.Implementations
{
    public class BookRepository : BaseRepository<Book, BookCreateDTO, BookGetDTO>, IBookRepository
    {
        public BookRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
