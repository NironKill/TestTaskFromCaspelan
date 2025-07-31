using Bookstore.Application.Responses;
using MediatR;

namespace Bookstore.Application.Handlers.Books.Queries.GetAll
{
    public class GetAllBookQuery : IRequest<ICollection<BookResponse>>
    {
        public string? Title { get; set; }

        public DateTime? PublicationDateFrom { get; set; }
        public DateTime? PublicationDateTo { get; set; }
    }
}
