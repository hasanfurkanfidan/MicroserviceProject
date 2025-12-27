using MediatR;
using MicroserviceProject.Order.Application.Features.Orders.GetOrders;
using MicroserviceProject.Shared.Extensions;

namespace MicroserviceProject.Order.Api.Endpoints.Orders
{
    public static class GetOrdersEndpoint
    {
        public static RouteGroupBuilder GetOrdersGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                GetOrdersQuery query = new();
                var result = await mediator.Send(query);
                return result.ToGenericResult();
            }).WithName("GetOrders");

            return group;
        }
    }
}
