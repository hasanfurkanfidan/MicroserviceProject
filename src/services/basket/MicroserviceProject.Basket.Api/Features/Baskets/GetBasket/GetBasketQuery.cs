using MediatR;
using MicroserviceProject.Basket.Api.Dtos;
using MicroserviceProject.Shared;

namespace MicroserviceProject.Basket.Api.Features.Baskets.GetBasket
{
    public record GetBasketQuery : IRequestByServiceResult<BasketDto>;
}
