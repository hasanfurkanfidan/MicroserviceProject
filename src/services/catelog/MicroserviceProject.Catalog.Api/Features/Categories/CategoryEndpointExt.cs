using Asp.Versioning.Builder;
using MicroserviceProject.Catalog.Api.Features.Categories.Create;
using MicroserviceProject.Catalog.Api.Features.Categories.GetAll;
using MicroserviceProject.Catalog.Api.Features.Categories.GetById;

namespace MicroserviceProject.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app,ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v:{version:apiversion}/categories").WithTags("Categories").WithApiVersionSet(apiVersionSet).CreateCategoryGroupItemEndpoint().GetAllCategoryGroupItemEndpoint().GetByIdCategoryGroupItemEndpoint();
        }
    }
}
