using AutoMapper;
using MicroserviceProject.Order.Application.Features.Orders.Create;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroserviceProject.Order.Application.Features.Orders
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<Domain.Entity.Order, GetOrders.GetOrdersQueryResponse>().ReverseMap();

            CreateMap<Domain.Entity.OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}
