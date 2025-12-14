using MediatR;
using MicroserviceProject.Shared.Extensions;
using MicroserviceProject.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceProject.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public static class DeleteBasketItemEndpoint
    {
        public static RouteGroupBuilder DeleteBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/item/{id:guid}", async (Guid id, [FromServices] IMediator mediator) =>
            {
                DeleteBasketItemCommand command = new DeleteBasketItemCommand(id);
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            }).WithName("DeleteBasketItem").MapToApiVersion(1, 0);

            return group;
        }
    }
}
