using Bookstore.Domain.Enums;
using FluentValidation;

namespace Bookstore.Application.Handlers.Books.Commands.Create
{
    public class CreateBookValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookValidator()
        {
            RuleFor(request => request.Author).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
            RuleFor(request => request.ISBN).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
            RuleFor(request => request.Price).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
            RuleFor(request => request.Title).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
        }
    }
}
