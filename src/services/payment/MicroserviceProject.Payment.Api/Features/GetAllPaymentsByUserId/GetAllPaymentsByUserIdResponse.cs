using MicroserviceProject.Payment.Api.Repositories;

namespace MicroserviceProject.Payment.Api.Features.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdResponse(
        Guid Id,
        string OrderCode,
        string Amount,
        DateTime Created,
        PaymentStatus Status);
}
