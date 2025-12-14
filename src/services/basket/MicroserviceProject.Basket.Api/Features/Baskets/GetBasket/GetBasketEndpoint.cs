using MediatR;
using MicroserviceProject.Shared.Extensions;

namespace MicroserviceProject.Basket.Api.Features.Baskets.GetBasket
{
    public static class GetBasketEndpoint
    {
        public static RouteGroupBuilder GetBasketGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user", async (IMediator mediator) =>
            {
                GetBasketQuery query = new GetBasketQuery();
                var result = await mediator.Send(query);
                return result.ToGenericResult();
            }).WithName("GetBasket").MapToApiVersion(1, 0);

            return group;
        }
    }

}
