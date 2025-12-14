using MicroserviceProject.Basket.Api.Const;
using MicroserviceProject.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MicroserviceProject.Basket.Api.Features.Baskets
{
    public class BasketService(IIdentityService identityService, IDistributedCache distributedCache)
    {
        private string GetCacheKey()
        {
            return String.Format(BasketConsts.BasketCacheKey, identityService.GetUserId);
        }
        public async Task<string?> GetBasketFromCache(CancellationToken cancellationToken = default)
        {
            return await distributedCache.GetStringAsync(GetCacheKey(), cancellationToken);
        }
        public async Task CreateCacheAsync(Data.Basket basket, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(GetCacheKey(), basketAsString, cancellationToken);
        }
    }
}
