using Asp.Versioning.Builder;
using MicroserviceProject.Order.Api.Endpoints.Orders;

namespace MicroserviceProject.Order.Api.Endpoints
{
    public static class OrderEndpointExt
    {
        public static void AddOrderGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v:{version:apiversion}/orders").WithTags("Orders").WithApiVersionSet(apiVersionSet).CreateOrderGroupItemEndpoint().GetOrdersGroupItemEndpoint().RequireAuthorization();
        }
    }
}
