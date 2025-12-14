using MediatR;
using MicroserviceProject.Basket.Api.Const;
using MicroserviceProject.Basket.Api.Dtos;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MicroserviceProject.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler(IDistributedCache distributedCache, IIdentityService identityService,BasketService basketService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basketAsString = await basketService.GetBasketFromCache();

            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult.Error("Basket not found", System.Net.HttpStatusCode.NotFound);
            }

            var currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

            var basketItemToDelete = currentBasket!.Items.FirstOrDefault(x => x.Id == request.Id);

            if (basketItemToDelete == null)
            {
                return ServiceResult.Error("Basket item not found", System.Net.HttpStatusCode.NotFound);
            }

            currentBasket.Items.Remove(basketItemToDelete);

            await basketService.CreateCacheAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
