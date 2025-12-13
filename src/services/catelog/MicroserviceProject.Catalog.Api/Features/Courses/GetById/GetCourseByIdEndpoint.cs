using MicroserviceProject.Catalog.Api.Features.Categories.GetById;
using MicroserviceProject.Catalog.Api.Features.Courses.Dtos;
using MicroserviceProject.Catalog.Api.Repositories;

namespace MicroserviceProject.Catalog.Api.Features.Courses.GetById
{
    public record GetCourseByIdQuery(Guid Id) : IRequestByServiceResult<CourseDto>;
    public class GetCourseByIdQueryHandler(AppDbContext context,IMapper mapper): IRequestHandler<GetCourseByIdQuery, ServiceResult<CourseDto>>
    {
        public async Task<ServiceResult<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course =  await context.Courses
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (course == null)
                return ServiceResult<CourseDto>.Error("Not Found", $"Course with Id '{request.Id}' does not exist.", System.Net.HttpStatusCode.NotFound);

            var category = await context.Categories
                .FirstAsync(c => c.Id == course.CategoryId, cancellationToken);

            course.Category = category!;
            var mappedCourse = mapper.Map<CourseDto>(course);
            return ServiceResult<CourseDto>.SuccessAsOk(mappedCourse);
        }
    }

    public static class GetCourseByIdEndpoint
    {
        public static RouteGroupBuilder GetByIdCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetCourseByIdQuery(id));

                return result.ToGenericResult();
            }).WithName("GetByIdCourse");

            return group;
        }
    }
}
