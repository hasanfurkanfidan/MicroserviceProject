using MicroserviceProject.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroserviceProject.Order.Application.Features.Orders.GetOrders
{
    public record GetOrdersQuery : IRequestByServiceResult<List<GetOrdersQueryResponse>>;

}
