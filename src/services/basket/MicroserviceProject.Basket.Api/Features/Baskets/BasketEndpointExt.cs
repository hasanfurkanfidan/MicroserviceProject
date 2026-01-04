using MicroserviceProject.Basket.Api.Features.Baskets.AddBasketItem;
using MicroserviceProject.Basket.Api.Features.Baskets.ApplyDiscountCoupon;
using MicroserviceProject.Basket.Api.Features.Baskets.DeleteBasketItem;
using MicroserviceProject.Basket.Api.Features.Baskets.GetBasket;
using MicroserviceProject.Basket.Api.Features.Baskets.RemoveDiscountCoupon;

namespace MicroserviceProject.Basket.Api.Features.Baskets
{
    public static class BasketEndpointExt
    {
        public static void AddBasketGroupEndpointExt(this WebApplication app, Asp.Versioning.Builder.ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v:{version:apiversion}/baskets")
                .WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .AddBasketItemGroupItemEndpoint()
                .DeleteBasketItemGroupItemEndpoint()
                .GetBasketGroupItemEndpoint()
                .ApplyDiscountCouponGroupItemEndpoint()
                .RemoveDiscountCouponGroupItemEndpoint()
                .RequireAuthorization("Password");
        }
    }
}
