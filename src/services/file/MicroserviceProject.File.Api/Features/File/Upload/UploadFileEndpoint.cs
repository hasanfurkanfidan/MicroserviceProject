using MediatR;
using MicroserviceProject.Shared.Extensions;

namespace MicroserviceProject.File.Api.Features.File.Upload
{
    public static class UploadFileEndpoint
    {
        public static RouteGroupBuilder UploadFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (IFormFile file, IMediator mediator) =>
            {
                var result = await mediator.Send(new UploadFileCommand(file));
                return result.ToGenericResult();
            }).WithName("UploadFile").MapToApiVersion(1, 0).DisableAntiforgery();

            return group;
        }
    }
}
