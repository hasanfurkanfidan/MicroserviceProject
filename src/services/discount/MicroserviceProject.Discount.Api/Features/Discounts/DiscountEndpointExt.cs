using Asp.Versioning.Builder;
using MicroserviceProject.Discount.Api.Features.Discounts.CreateDiscount;
using MicroserviceProject.Discount.Api.Features.Discounts.GetDiscountByCode;

namespace MicroserviceProject.Discount.Api.Features.Discounts
{
    public static class DiscountEndpointExt
    {
        public static void AddDiscountGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v:{version:apiversion}/discounts").WithTags("Discounts").WithApiVersionSet(apiVersionSet).CreateDiscountGroupItemEndpoint().GetDiscountByCodeGroupItemEndpoint().RequireAuthorization();
        }
    }
}
