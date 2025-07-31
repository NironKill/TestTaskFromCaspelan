using MediatR;

namespace Bookstore.Application.Handlers.Books.Commands.Create
{
    public class CreateBookCommand : IRequest<Guid>
    {
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; } = "No publisher";
        public string ImageUrl { get; set; } = "No image";
    }
}
