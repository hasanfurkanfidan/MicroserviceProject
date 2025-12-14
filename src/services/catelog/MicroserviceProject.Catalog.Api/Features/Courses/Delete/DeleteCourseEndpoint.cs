using MicroserviceProject.Catalog.Api.Features.Courses.GetById;
using MicroserviceProject.Catalog.Api.Repositories;

namespace MicroserviceProject.Catalog.Api.Features.Courses.Delete
{
    public record DeleteCourseCommand(Guid Id) : IRequestByServiceResult;

    public class DeleteCourseCommandHandler(AppDbContext context) : IRequestHandler<DeleteCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await context.FindAsync<Course>();
            if (course == null)
            {
                return ServiceResult.Error("NotFound", "NotFound", System.Net.HttpStatusCode.NotFound);
            }

            context.Remove(course);

            await context.SaveChangesAsync();

            return ServiceResult.SuccessAsNoContent();
        }
    }
    public static class DeleteCourseEndpoint
    {
        public static RouteGroupBuilder DeleteCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new DeleteCourseCommand(id));

                return result.ToGenericResult();
            }).MapToApiVersion(1, 0).WithName("DeleteCourse");

            return group;
        }
    }
}
