using MicroserviceProject.Shared;

namespace MicroserviceProject.Payment.Api.Features.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdQuery : IRequestByServiceResult<List<GetAllPaymentsByUserIdResponse>>;
}
