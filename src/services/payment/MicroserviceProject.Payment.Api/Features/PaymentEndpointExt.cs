using Asp.Versioning.Builder;
using MicroserviceProject.Payment.Api.Features.Create;
using MicroserviceProject.Payment.Api.Features.GetAllPaymentsByUserId;

namespace MicroserviceProject.Payment.Api.Features
{
    public static class PaymentEndpointExt
    {
        public static void AddPaymentGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/payments").WithTags("payments").WithApiVersionSet(apiVersionSet)
                .CreatePaymentGroupItemEndpoint().GetAllPaymentsByUserIdGroupItemEndpoint().RequireAuthorization("Password");
        }
    }
}
