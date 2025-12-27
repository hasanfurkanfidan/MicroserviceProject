namespace MicroserviceProject.Order.Application.Contracts.Repositories
{
    public interface IOrderRepository : IGenericRepository<Guid, Domain.Entity.Order>
    {
        Task<List<Domain.Entity.Order>> GetOrdersByUserId(Guid buyerId);

    }
}
