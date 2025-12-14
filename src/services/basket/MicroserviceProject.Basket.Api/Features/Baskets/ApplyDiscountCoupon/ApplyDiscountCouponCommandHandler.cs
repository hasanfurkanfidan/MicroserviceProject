using MediatR;
using MicroserviceProject.Basket.Api.Dtos;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;

namespace MicroserviceProject.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandHandler(IIdentityService identityService, IDistributedCache distributedCache, BasketService basketService) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var basketAsString = await basketService.GetBasketFromCache();

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

            await basketService.CreateCacheAsync(basket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
