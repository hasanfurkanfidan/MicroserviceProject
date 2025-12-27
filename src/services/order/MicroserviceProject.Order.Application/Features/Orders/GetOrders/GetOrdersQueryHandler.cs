using AutoMapper;
using MediatR;
using MicroserviceProject.Order.Application.Contracts.Repositories;
using MicroserviceProject.Order.Application.Features.Orders.Create;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Services;

namespace MicroserviceProject.Order.Application.Features.Orders.GetOrders
{
    public class GetOrdersQueryHandler(IOrderRepository orderRepository, IIdentityService identityService, IMapper mapper) : IRequestHandler<GetOrdersQuery, ServiceResult<List<GetOrdersQueryResponse>>>
    {
        public async Task<ServiceResult<List<GetOrdersQueryResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var userId = identityService.GetUserId;
            var orders = await orderRepository.GetOrdersByUserId(userId);

            var response = orders.Select(o => new GetOrdersQueryResponse(o.Created,o.TotalPrice,mapper.Map<List<OrderItemDto>>(o.OrderItems)));


            return ServiceResult<List<GetOrdersQueryResponse>>.SuccessAsOk(response.ToList());
        }
    }
}
