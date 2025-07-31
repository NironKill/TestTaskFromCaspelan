using Bookstore.Application.Handlers.Books.Commands.Create;
using Bookstore.Application.Handlers.Books.Queries.GetAll;
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

        public async Task<ICollection<BookResponse>> GetAllByFilter(GetAllBookQuery query, Func<Book, BookResponse> map, CancellationToken cancellationToken)
        {
            IQueryable<Book> bookQuery = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(query.Title))
                bookQuery = bookQuery.Where(x => x.Title.StartsWith(query.Title)).AsQueryable();

            if (query.PublicationDateFrom is not null)
                bookQuery = bookQuery.Where(x => x.PublishedIn >= query.PublicationDateFrom).AsQueryable();
            if (query.PublicationDateTo is not null)
                bookQuery = bookQuery.Where(x => x.PublishedIn <= query.PublicationDateTo).AsQueryable();

            List<Book> books = await bookQuery.ToListAsync();
            return books.Select(map).ToList();
        }
    }
}
