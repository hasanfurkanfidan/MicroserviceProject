using MicroserviceProject.Catalog.Api.Features.Courses;
using MicroserviceProject.Catalog.Api.Repositories;

namespace MicroserviceProject.Catalog.Api.Features.Categories
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = default!;
        public List<Course>? Courses { get; set; }
    }
}
