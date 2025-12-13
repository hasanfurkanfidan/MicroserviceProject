using MicroserviceProject.Catalog.Api.Features.Courses.Create;

namespace MicroserviceProject.Catalog.Api.Features.Courses.Update
{
    public static class UpdateCourseEndpoint
    {
        public static RouteGroupBuilder UpdateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (UpdateCourseCommand Command, IMediator mediator) =>
            {
                var result = await mediator.Send(Command);
                return result.ToGenericResult();
            }).WithName("UpdateCourse").AddEndpointFilter<ValidationFilter<UpdateCourseCommand>>();

            return group;
        }
    }
}
