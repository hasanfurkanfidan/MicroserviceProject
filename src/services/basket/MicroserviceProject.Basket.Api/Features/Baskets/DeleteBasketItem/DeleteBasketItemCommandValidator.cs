using FluentValidation;

namespace MicroserviceProject.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public class DeleteBasketItemCommandValidator : AbstractValidator<DeleteBasketItemCommand>
    {
        public DeleteBasketItemCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("CourseId required");
        }
    }
}
