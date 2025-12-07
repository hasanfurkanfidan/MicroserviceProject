using MediatR;
using MicroserviceProject.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MicroserviceProject.Catalog.Api.Features.Categories.Create
{
    public static class CreateCategoryEndpoint
    {
        public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCategoryCommand Command, IMediator mediator) =>
            {
                var result = await mediator.Send(Command);
                return result.ToGenericResult();
            });

            return group;
        }
    }
}
