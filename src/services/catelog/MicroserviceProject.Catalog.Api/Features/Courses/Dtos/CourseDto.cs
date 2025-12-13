using MicroserviceProject.Catalog.Api.Features.Categories.Dtos;

namespace MicroserviceProject.Catalog.Api.Features.Courses.Dtos
{
    public record CourseDto(Guid Id, string Name, string Description, string ImageUrl, decimal Price, CategoryDto Category, FeatureDto Feature);
}
