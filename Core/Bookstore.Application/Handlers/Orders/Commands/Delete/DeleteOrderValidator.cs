using Bookstore.Application.Interfaces;
using Bookstore.Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Handlers.Orders.Commands.Delete
{
    public class DeleteOrderValidator : AbstractValidator<DeleteOrderCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteOrderValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.Id).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.Id).MustAsync(OrderExists)
                .WithMessage("This order does not exist.")
                .WithErrorCode($"{StatusCode.NotFound.GetHashCode()}");
        }

        private async Task<bool> OrderExists(Guid id, CancellationToken cancellation) =>
           await _context.Orders.AnyAsync(x => x.Id == id, cancellation);
    }
}
