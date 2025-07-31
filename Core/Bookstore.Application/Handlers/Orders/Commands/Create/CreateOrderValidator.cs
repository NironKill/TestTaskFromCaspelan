using Bookstore.Application.Interfaces;
using Bookstore.Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Handlers.Orders.Commands.Create
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateOrderValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.BookIds).NotNull().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
            RuleFor(request => request.BookIds).MustAsync(BookExists)
                .WithMessage("You have specified a non-existent book.")
                .WithErrorCode($"{StatusCode.NotFound.GetHashCode()}");
        }

        private async Task<bool> BookExists(HashSet<Guid> ids, CancellationToken cancellation)
        {
            foreach (Guid id in ids)
            {
                bool exist = await _context.Books.AnyAsync(x => x.Id == id, cancellation);
                if (!exist) 
                    return false;
            }
            return true;
        }
    }
}
