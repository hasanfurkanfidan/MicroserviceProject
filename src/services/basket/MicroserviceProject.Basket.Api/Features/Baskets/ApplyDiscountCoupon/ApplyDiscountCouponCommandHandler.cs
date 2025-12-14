using MediatR;
using MicroserviceProject.Basket.Api.Const;
using MicroserviceProject.Basket.Api.Dtos;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;

namespace MicroserviceProject.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandHandler(IIdentityService identityService, IDistributedCache distributedCache) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            Guid userId = identityService.GetUserId;
            var cacheKey = String.Format(BasketConsts.BasketCacheKey, userId);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);
            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult<BasketDto>.Error("Basket Not Found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

            if (basket!.Items.Any())
            {
                return ServiceResult<BasketDto>.Error("Basket is empty, cannot apply discount", HttpStatusCode.NotFound);
            }


            basket!.ApplyNewDiscount(request.Coupon, request.DiscountRate);

            await CreateCacheAsync(basket, cacheKey, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }

        private async Task CreateCacheAsync(Data.Basket basket, string cacheKey, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);
        }
    }
}
