using MediatR;
using MicroserviceProject.Basket.Api.Const;
using MicroserviceProject.Basket.Api.Dtos;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;

namespace MicroserviceProject.Basket.Api.Features.Baskets.GetBasket
{
    public class GetBasketQueryHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            Guid userId = identityService.GetUserId;
            var cacheKey = String.Format(BasketConsts.BasketCacheKey, userId);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);
            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult<BasketDto>.Error("Basket Not Found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<BasketDto>(basketAsString);

            return ServiceResult<BasketDto>.SuccessAsOk(basket!);
        }
    }
}
