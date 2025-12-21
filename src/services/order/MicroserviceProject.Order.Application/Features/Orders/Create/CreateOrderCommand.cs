using MicroserviceProject.Shared;

namespace MicroserviceProject.Order.Application.Features.Orders.Create
{
    public record CreateOrderCommand(float? DiscountRate, AddressDto Address, PaymentDto Payment, List<OrderItemDto> OrderItems) : IRequestByServiceResult;
    public record AddressDto(string Line, string Province, string District, string ZipCode, string Street);
    public record PaymentDto(string CardNumber, string CardHolderName, string Expiration, string Cvc, decimal Amount);
    public record OrderItemDto(Guid ProductId, string ProductName, decimal UnitPrice);
}
