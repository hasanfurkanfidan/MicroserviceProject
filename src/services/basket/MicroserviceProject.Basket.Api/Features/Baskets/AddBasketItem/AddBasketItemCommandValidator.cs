using FluentValidation;

namespace MicroserviceProject.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandValidator : AbstractValidator<AddBasketItemCommand>
    {
        public AddBasketItemCommandValidator()
        {
            RuleFor(p => p.CourseId).NotEmpty().WithMessage("CourseId required");
        }
    }
}
