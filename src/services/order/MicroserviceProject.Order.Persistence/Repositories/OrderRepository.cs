using MicroserviceProject.Order.Application.Contracts.Repositories;

namespace MicroserviceProject.Order.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Guid, Domain.Entity.Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
