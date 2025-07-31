using Bookstore.Application.Interfaces;
using Bookstore.Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Handlers.Books.Commands.Delete
{
    public class DeleteBookValidator : AbstractValidator<DeleteBookCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteBookValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.Id).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.Id).MustAsync(BookExists)
                .WithMessage("This book does not exist.")
                .WithErrorCode($"{StatusCode.NotFound.GetHashCode()}");
        }

        private async Task<bool> BookExists(Guid id, CancellationToken cancellation) =>
           await _context.Books.AnyAsync(x => x.Id == id, cancellation);
    }
}
