using MediatR;
using MicroserviceProject.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceProject.File.Api.Features.File.Delete
{
    public static class DeleteFileEndpoint
    {
        public static RouteGroupBuilder DeleteFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromBody] DeleteFileCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            }).WithName("DeleteFile").MapToApiVersion(1, 0).DisableAntiforgery();

            return group;
        }
    }
}
