using MediatR;
using MicroserviceProject.Basket.Api.Features.Baskets.AddBasketItem;
using MicroserviceProject.Shared.Extensions;
using MicroserviceProject.Shared.Filters;

namespace MicroserviceProject.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public static class ApplyDiscountCouponEndpoint
    {
        public static RouteGroupBuilder ApplyDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/apply-discount-coupon", async (ApplyDiscountCouponCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            }).WithName("ApplyDiscountCoupon").MapToApiVersion(1, 0).AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommand>>();

            return group;
        }
    }
}
