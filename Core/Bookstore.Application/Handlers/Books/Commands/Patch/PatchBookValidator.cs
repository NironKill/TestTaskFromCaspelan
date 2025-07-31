using Bookstore.Application.Interfaces;
using Bookstore.Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Handlers.Books.Commands.Patch
{
    public class PatchBookValidator : AbstractValidator<PatchBookCommand>
    {
        private readonly IApplicationDbContext _context;

        public PatchBookValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.Id).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
            RuleFor(request => request.Id).MustAsync(BookExists)
                .WithMessage("This book does not exist.")
                .WithErrorCode($"{StatusCode.NotFound.GetHashCode()}");

            RuleFor(request => request.Price).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
        }

        private async Task<bool> BookExists(Guid id, CancellationToken cancellation) =>
           await _context.Books.AnyAsync(x => x.Id == id, cancellation);
    }
}
