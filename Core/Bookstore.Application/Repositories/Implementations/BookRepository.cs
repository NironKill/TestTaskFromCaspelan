using Bookstore.Application.Handlers.Books.Commands.Create;
using Bookstore.Application.Interfaces;
using Bookstore.Application.Repositories.Abstract;
using Bookstore.Application.Repositories.Interfaces;
using Bookstore.Application.Responses;
using Bookstore.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Repositories.Implementations
{
    public class BookRepository : BaseRepository<Book, CreateBookCommand, BookResponse>, IBookRepository
    {
        public BookRepository(IApplicationDbContext context) : base(context) { }

        public async Task<ICollection<BookResponse>> GetAllByBookOrder(Guid orderId, CancellationToken cancellationToken) =>
            await _context.BookOrders.Where(x => x.OrderId == orderId).Select(bo => new BookResponse
            {
                Id = bo.Book.Id,
                Author = bo.Book.Author,
                ISBN = bo.Book.ISBN,
                Price = bo.Price,
                Title = bo.Book.Title,
                Publisher = bo.Book.Publisher,
                ImageUrl = bo.Book.ImageUrl,
                PublishedIn = bo.Book.PublishedIn.ToString("g"),
            }).ToListAsync(cancellationToken); 
    }
}
