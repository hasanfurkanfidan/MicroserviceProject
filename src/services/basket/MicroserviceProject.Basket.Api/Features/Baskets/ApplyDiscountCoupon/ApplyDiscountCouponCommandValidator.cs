using FluentValidation;

namespace MicroserviceProject.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandValidator : AbstractValidator<ApplyDiscountCouponCommand>
    {
        public ApplyDiscountCouponCommandValidator()
        {
            RuleFor(p => p.Coupon).NotEmpty().WithMessage("Coupon is required");
            RuleFor(p => p.DiscountRate).GreaterThan(0).WithMessage("DiscountRate must be greater than 0");
        }
    }
}
