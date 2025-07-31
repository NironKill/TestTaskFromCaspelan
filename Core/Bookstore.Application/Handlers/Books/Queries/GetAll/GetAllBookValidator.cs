using Bookstore.Domain.Enums;
using FluentValidation;

namespace Bookstore.Application.Handlers.Books.Queries.GetAll
{
    public class GetAllBookValidator : AbstractValidator<GetAllBookQuery>
    {
        public GetAllBookValidator()
        {
            RuleFor(request => request).Must(DateCheck)
                .WithMessage("You have entered an incorrect order date.")
                .WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
        }

        private bool DateCheck(GetAllBookQuery query)
        {
            bool isDateCorrect = true;
            if (query.PublicationDateFrom is not null && query.PublicationDateTo is not null)
                isDateCorrect = query.PublicationDateFrom <= query.PublicationDateTo;
            if (query.PublicationDateFrom is not null)
                isDateCorrect = query.PublicationDateFrom <= DateTime.UtcNow;
            if (query.PublicationDateTo is not null)
                isDateCorrect = query.PublicationDateTo <= DateTime.UtcNow;

            return isDateCorrect;
        }
    }
}
