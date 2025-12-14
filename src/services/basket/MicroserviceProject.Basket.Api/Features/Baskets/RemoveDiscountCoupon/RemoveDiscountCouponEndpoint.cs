using MediatR;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Extensions;
using MicroserviceProject.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;

namespace MicroserviceProject.Basket.Api.Features.Baskets.RemoveDiscountCoupon
{
    public record RemoveDiscountCouponCommand : IRequestByServiceResult;
    public class RemoveDiscountCouponCommandHandler(IDistributedCache distributedCache, IIdentityService identityService, BasketService basketService) : IRequestHandler<RemoveDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(RemoveDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var basketAsString = await basketService.GetBasketFromCache();

            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult.Error("Basket Not Found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

            basket!.ClearDiscount();

            await basketService.CreateCacheAsync(basket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }

    public static class RemoveDiscountCouponEndpoint
    {
        public static RouteGroupBuilder RemoveDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/remove-discount-coupon", async (IMediator mediator) =>
            {
                RemoveDiscountCouponCommand command = new RemoveDiscountCouponCommand();
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            }).WithName("RemoveDiscountCoupon").MapToApiVersion(1, 0);

            return group;
        }

    }
}
