using MicroserviceProject.Shared;

namespace MicroserviceProject.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public record DeleteBasketItemCommand(Guid Id) : IRequestByServiceResult;
}
