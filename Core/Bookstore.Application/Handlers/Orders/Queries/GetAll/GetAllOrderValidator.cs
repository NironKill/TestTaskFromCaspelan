using Bookstore.Domain.Enums;
using FluentValidation;

namespace Bookstore.Application.Handlers.Orders.Queries.GetAll
{
    public class GetAllOrderValidator : AbstractValidator<GetAllOrderQuery>
    {
        public GetAllOrderValidator()
        {
            RuleFor(request => request).Must(DateCheck)
                .WithMessage("You have entered an incorrect order date.")
                .WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request).Must(NumberCheck)
                .WithMessage("You made a mistake when filtering by order number.")
                .WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
        }

        private bool DateCheck(GetAllOrderQuery query)
        {
            bool isDateCorrect = true;
            if (query.OrderDateFrom is not null && query.OrderDateTo is not null)            
                isDateCorrect = query.OrderDateFrom <= query.OrderDateTo;
            if (query.OrderDateFrom is not null)
                isDateCorrect = query.OrderDateFrom <= DateTime.UtcNow;
            if (query.OrderDateTo is not null)
                isDateCorrect = query.OrderDateTo <= DateTime.UtcNow;

            return isDateCorrect;
        }
        private bool NumberCheck(GetAllOrderQuery query)
        {
            bool result = true;
            if (query.OrderNumberFrom is not null && query.OrderNumberTo is not null)
                result = query.OrderNumberFrom <= query.OrderNumberTo;

            return result;
        }
    }
}
