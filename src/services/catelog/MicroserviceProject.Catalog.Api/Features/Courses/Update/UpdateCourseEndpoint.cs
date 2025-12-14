using MicroserviceProject.Catalog.Api.Features.Courses.Create;

namespace MicroserviceProject.Catalog.Api.Features.Courses.Update
{
    public static class UpdateCourseEndpoint
    {
        public static RouteGroupBuilder UpdateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/", async (UpdateCourseCommand Command, IMediator mediator) =>
            {
                var result = await mediator.Send(Command);
                return result.ToGenericResult();
            }).MapToApiVersion(1, 0).WithName("UpdateCourse").AddEndpointFilter<ValidationFilter<UpdateCourseCommand>>();

            return group;
        }
    }
}
