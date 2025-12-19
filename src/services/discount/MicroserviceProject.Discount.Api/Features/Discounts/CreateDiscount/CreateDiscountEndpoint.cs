using MediatR;
using MicroserviceProject.Shared.Filters;

namespace MicroserviceProject.Discount.Api.Features.Discounts.CreateDiscount
{
    public static class CreateDiscountEndpoint
    {
        public static RouteGroupBuilder CreateDiscountGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateDiscountCommand Command, IMediator mediator) =>
            {
                var result = await mediator.Send(Command);
                return result.ToGenericResult();
            }).WithName("CreateDiscount").MapToApiVersion(1, 0).AddEndpointFilter<ValidationFilter<CreateDiscountCommand>>();

            return group;
        }
    }
}
