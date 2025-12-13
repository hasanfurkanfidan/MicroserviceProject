using MicroserviceProject.Catalog.Api.Features.Categories.Create;
using MicroserviceProject.Catalog.Api.Features.Categories.GetAll;
using MicroserviceProject.Catalog.Api.Features.Categories.GetById;

namespace MicroserviceProject.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/categories").WithTags("Categories").CreateCategoryGroupItemEndpoint().GetAllCategoryGroupItemEndpoint().GetByIdCategoryGroupItemEndpoint();
        }
    }
}
