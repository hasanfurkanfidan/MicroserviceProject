
using MicroserviceProject.Catalog.Api.Repositories;
using System.Net;

namespace MicroserviceProject.Catalog.Api.Features.Courses.Create
{
    public class CreateCourseCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var hasCategory = await context.Categories.AnyAsync(c => c.Id == request.CategoryId, cancellationToken);
            if (!hasCategory)
                return ServiceResult<Guid>.Error("Category Not Found", $"Category with Id '{request.CategoryId}' does not exist.", HttpStatusCode.NotFound);

            var hasSameNameCourse = await context.Courses.AnyAsync(c => c.Name == request.Name, cancellationToken);
            if (hasSameNameCourse)
                return ServiceResult<Guid>.Error("Duplicate Course", $"Course with name '{request.Name}' already exists.", HttpStatusCode.BadRequest);

            var newCourse = mapper.Map<Course>(request);
            newCourse.Created = DateTime.UtcNow;
            newCourse.Id = NewId.NextSequentialGuid();

            var feature = new Feature
            {
                Duration = 10,
                EducatorFullName = "Hasan Furkan Fidan",
                Rating = 0
            };

            newCourse.Feature = feature;
            await context.Courses.AddAsync(newCourse, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<Guid>.SuccessAsCreated(newCourse.Id, $"/api/courses/{newCourse.Id}");
        }
    }
}
