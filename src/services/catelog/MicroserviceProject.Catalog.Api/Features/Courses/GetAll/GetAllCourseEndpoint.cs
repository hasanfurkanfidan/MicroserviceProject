using MicroserviceProject.Catalog.Api.Features.Categories.GetAll;
using MicroserviceProject.Catalog.Api.Features.Courses.Dtos;
using MicroserviceProject.Catalog.Api.Repositories;

namespace MicroserviceProject.Catalog.Api.Features.Courses.GetAll
{
    public record GetAllCourseQuery() : IRequestByServiceResult<List<CourseDto>>;

    public class GetAllCourseQueryHandler(
        AppDbContext context,
        IMapper mapper) : IRequestHandler<GetAllCourseQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCourseQuery request, CancellationToken cancellationToken)
        {
            var courses = await context.Courses
                .ToListAsync();

            var categories = await context.Categories.ToListAsync();

            foreach (var course in courses)
            {
                course.Category = categories.First(c => c.Id == course.CategoryId);   
            }

            var mappedCourses = mapper.Map<List<CourseDto>>(courses);

            return ServiceResult<List<CourseDto>>.SuccessAsOk(mappedCourses);
        }
    }
    public static class GetAllCourseEndpoint
    {
        public static RouteGroupBuilder GetAllCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllCourseQuery());
                return result.ToGenericResult();
            }).WithName("GetAllCourse");

            return group;
        }
    }
}
