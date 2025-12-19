using FluentValidation;

namespace MicroserviceProject.Discount.Api.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotNull()
                .WithMessage("Code is required!");
        }
    }
}
