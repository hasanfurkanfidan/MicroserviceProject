using MediatR;
using MicroserviceProject.Order.Application.Features.Orders.Create;
using MicroserviceProject.Shared.Filters;
using MicroserviceProject.Shared.Extensions;
namespace MicroserviceProject.Order.Api.Endpoints.Orders
{
    public static class CreateOrderEndpoint
    {
        public static RouteGroupBuilder CreateOrderGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateOrderCommand Command, IMediator mediator) =>
            {
                var result = await mediator.Send(Command);
                return result.ToGenericResult();
            }).WithName("CreateOrder").AddEndpointFilter<ValidationFilter<CreateOrderCommand>>();

            return group;
        }
    }
}
