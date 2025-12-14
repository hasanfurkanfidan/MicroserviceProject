using MediatR;
using MicroserviceProject.Basket.Api.Const;
using MicroserviceProject.Basket.Api.Dtos;
using MicroserviceProject.Basket.Api.Features.Baskets.AddBasketItem;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Extensions;
using MicroserviceProject.Shared.Filters;
using MicroserviceProject.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;

namespace MicroserviceProject.Basket.Api.Features.Baskets.RemoveDiscountCoupon
{
    public record RemoveDiscountCouponCommand : IRequestByServiceResult;
    public class RemoveDiscountCouponCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<RemoveDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(RemoveDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            Guid userId = identityService.GetUserId;
            var cacheKey = String.Format(BasketConsts.BasketCacheKey, userId);

            var basketAsJson = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult.Error("Basket Not Found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

            basket!.ClearDiscount();

            await CreateCacheAsync(basket, cacheKey, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }

        private async Task CreateCacheAsync(Data.Basket basket, string cacheKey, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);
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
