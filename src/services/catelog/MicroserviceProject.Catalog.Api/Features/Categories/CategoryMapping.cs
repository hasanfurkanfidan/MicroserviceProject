using MicroserviceProject.Catalog.Api.Features.Categories.Dtos;

namespace MicroserviceProject.Catalog.Api.Features.Categories
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
