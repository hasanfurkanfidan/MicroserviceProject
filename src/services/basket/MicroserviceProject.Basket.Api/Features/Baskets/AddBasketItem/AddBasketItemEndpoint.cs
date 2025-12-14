using MediatR;
using MicroserviceProject.Shared.Extensions;
using MicroserviceProject.Shared.Filters;

namespace MicroserviceProject.Basket.Api.Features.Baskets.AddBasketItem
{
    public static class AddBasketItemEndpoint
    {
        public static RouteGroupBuilder AddBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/item", async (AddBasketItemCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            }).WithName("AddBasketItem").MapToApiVersion(1, 0).AddEndpointFilter<ValidationFilter<AddBasketItemCommand>>();

            return group;
        }
    }
}
