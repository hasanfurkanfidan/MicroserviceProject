using MediatR;
using MicroserviceProject.Order.Application.Contracts.Repositories;
using MicroserviceProject.Order.Application.Contracts.UnitOfWorks;
using MicroserviceProject.Order.Domain.Entity;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Services;
using System.Data;

namespace MicroserviceProject.Order.Application.Features.Orders.Create
{
    public class CreateOrderCommandHandler(IGenericRepository<Guid, Domain.Entity.Order> orderRepository, IGenericRepository<int, Address> addressRepository, IIdentityService identityService, IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var address = new Domain.Entity.Address
            {
                District = request.Address.District,
                Province = request.Address.Province,
                Line = request.Address.Line,
                Street = request.Address.Street,
                ZipCode = request.Address.ZipCode
            };

            addressRepository.Add(address);

            await unitOfWork.CommitTransactionAsync(cancellationToken);

            var order = Domain.Entity.Order.CreateUnPaidOrder(identityService.GetUserId, request.DiscountRate);

            foreach (var item in request.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice);
            }

            order.Address = address;
            orderRepository.Add(order);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            var paymentId = Guid.Empty;
            order.SetPaidStatus(Guid.NewGuid());

            orderRepository.Update(order);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
