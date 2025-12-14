using MediatR;
using MicroserviceProject.Basket.Api.Const;
using MicroserviceProject.Basket.Api.Dtos;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MicroserviceProject.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(IDistributedCache distributedCache,IIdentityService identityService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            Guid userId = identityService.GetUserId;
            var cacheKey = String.Format(BasketConsts.BasketCacheKey, userId);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

            Data.Basket? currentBasket;

            var newBasketItem = new Data.BasketItem(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice, null);
            
            if (string.IsNullOrEmpty(basketAsString))
            {
                currentBasket = new Data.Basket(userId, [newBasketItem]);

                await CreateCacheAsync(currentBasket!, cacheKey, cancellationToken);

                return ServiceResult.SuccessAsNoContent();
            }

            currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);
            var existingBasketItem = currentBasket!.Items.FirstOrDefault(bi => bi.Id == request.CourseId);

            if (existingBasketItem is not null)
            {
                currentBasket?.Items.Remove(existingBasketItem);
            }

            currentBasket!.Items.Add(newBasketItem);

            currentBasket!.ApplyAvailableDiscount();

            await CreateCacheAsync(currentBasket!, cacheKey, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }


        private async Task CreateCacheAsync(Data.Basket basket, string cacheKey, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);
        }
    }
}
