using Microsoft.AspNetCore.Mvc;

namespace MicroserviceProject.Catalog.Api.Features.Courses.Create
{
    public static class CreateCourseEndpoint
    {
        public static RouteGroupBuilder CreateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCourseCommand Command, IMediator mediator) =>
            {
                var result = await mediator.Send(Command);
                return result.ToGenericResult();
            }).WithName("CreateCourse").Produces<Guid>(StatusCodes.Status201Created).Produces(StatusCodes.Status404NotFound).Produces<ProblemDetails>(StatusCodes.Status400BadRequest).MapToApiVersion(1, 0).AddEndpointFilter<ValidationFilter<CreateCourseCommand>>();

            return group;
        }
    }
}
