using MicroserviceProject.Catalog.Api.Features.Categories.Create;
using MicroserviceProject.Shared.Filters;

namespace MicroserviceProject.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/categories").CreateCategoryGroupItemEndpoint();
        }
    }
}
