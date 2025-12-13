
using MicroserviceProject.Catalog.Api.Repositories;

namespace MicroserviceProject.Catalog.Api.Features.Courses.Update
{
    public class UpdateCourseCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<UpdateCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FindAsync(request.Id, cancellationToken);
            if (course is null)
                return ServiceResult.ErrorAsNotFound();

            course = mapper.Map<Course>(request);

            context.Update(course);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
