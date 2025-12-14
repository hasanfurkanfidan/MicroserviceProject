using MicroserviceProject.Catalog.Api.Features.Courses.Dtos;
using MicroserviceProject.Catalog.Api.Repositories;

namespace MicroserviceProject.Catalog.Api.Features.Courses.GetAllByUserId
{

    public record GetAllCoursesByUserIdQuery(Guid UserId) : IRequestByServiceResult<List<CourseDto>>;
    public class GetAllCoursesByUserIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCoursesByUserIdQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCoursesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var courses = await context.Courses
                .Where(c => c.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            var mappedCourses = mapper.Map<List<CourseDto>>(courses);
            return ServiceResult<List<CourseDto>>.SuccessAsOk(mappedCourses);
        }
    }

    public static class GetAllCoursesByUserIdEndpoint
    {
        public static RouteGroupBuilder GetAllCoursesByUserIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("user/{userId:guid}", async (Guid userId, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllCoursesByUserIdQuery(userId));

                return result.ToGenericResult();
            }).MapToApiVersion(1, 0).WithName("GetAllCoursesByUserId");

            return group;
        }
    }
}
