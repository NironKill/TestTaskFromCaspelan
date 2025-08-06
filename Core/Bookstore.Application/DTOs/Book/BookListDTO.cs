using Bookstore.Application.Responses;

namespace Bookstore.Application.DTOs.Book
{
    public class BookListDTO
    {
        public ICollection<BookResponse> Books { get; set; }
    }
}
