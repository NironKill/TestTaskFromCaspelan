using Bookstore.Application.DTOs.Book;
using Bookstore.Application.Handlers.Books.Commands.Create;
using Bookstore.Application.Handlers.Books.Queries.GetAll;
using Bookstore.Application.Interfaces;
using Bookstore.Application.Repositories.Abstract;
using Bookstore.Application.Repositories.Interfaces;
using Bookstore.Application.Responses;
using Bookstore.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bookstore.Application.Repositories.Implementations
{
    public class BookRepository : BaseRepository<Book, CreateBookCommand, BookResponse>, IBookRepository
    {
        public BookRepository(IApplicationDbContext context) : base(context) { }

        public async Task<ICollection<BookGetDTO>> GetAllByOrderId(Expression<Func<BookOrder, bool>> predicate, CancellationToken cancellationToken) =>
            await _context.BookOrders.AsSplitQuery().Where(predicate).Select(bo => new BookGetDTO
            {
                Id = bo.Book.Id,
                OrderId = bo.OrderId,
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
            IQueryable<Book> bookQuery = _context.Books;

            if (!string.IsNullOrEmpty(query.Title))
                bookQuery = bookQuery.Where(x => x.Title.StartsWith(query.Title));

            if (query.PublicationDateFrom is not null)
                bookQuery = bookQuery.Where(x => x.PublishedIn >= query.PublicationDateFrom);
            if (query.PublicationDateTo is not null)
                bookQuery = bookQuery.Where(x => x.PublishedIn <= query.PublicationDateTo);

            List<Book> books = await bookQuery.ToListAsync(cancellationToken);
            return books.Select(map).ToList();
        }
    }
}
