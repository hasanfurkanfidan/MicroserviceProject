using MicroserviceProject.Order.Application.Features.Orders.Create;

namespace MicroserviceProject.Order.Application.Features.Orders.GetOrders
{
    public record GetOrdersQueryResponse(DateTime OrderDate,decimal TotalPrice,List<OrderItemDto> Items);
}
