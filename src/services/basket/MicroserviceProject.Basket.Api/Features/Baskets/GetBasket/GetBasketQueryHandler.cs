using AutoMapper;
using MediatR;
using MicroserviceProject.Basket.Api.Dtos;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;

namespace MicroserviceProject.Basket.Api.Features.Baskets.GetBasket
{
    public class GetBasketQueryHandler(IDistributedCache distributedCache, IIdentityService identityService, IMapper mapper, BasketService basketService) : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basketAsString = await basketService.GetBasketFromCache();
            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult<BasketDto>.Error("Basket Not Found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

            var basketDto = mapper.Map<BasketDto>(basket);

            return ServiceResult<BasketDto>.SuccessAsOk(basketDto);
        }


    }
}
