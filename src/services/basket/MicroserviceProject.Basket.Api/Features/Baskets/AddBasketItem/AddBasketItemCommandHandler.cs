using MediatR;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MicroserviceProject.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(IDistributedCache distributedCache, IIdentityService identityService, BasketService basketService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basketAsString = await basketService.GetBasketFromCache();

            Data.Basket? currentBasket;

            var newBasketItem = new Data.BasketItem(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice, null);

            if (string.IsNullOrEmpty(basketAsString))
            {
                currentBasket = new Data.Basket(identityService.GetUserId, [newBasketItem]);

                await basketService.CreateCacheAsync(currentBasket!, cancellationToken);

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

            await basketService.CreateCacheAsync(currentBasket!, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
