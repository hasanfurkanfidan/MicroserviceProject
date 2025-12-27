using MicroserviceProject.Order.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceProject.Order.Persistence.Repositories
{
    public class OrderRepository(AppDbContext context) : GenericRepository<Guid, Domain.Entity.Order>(context), IOrderRepository
    {
        public async Task<List<Domain.Entity.Order>> GetOrdersByUserId(Guid buyerId)
        {
            return await context.Orders.Where(o => o.BuyerId == buyerId)
                .Include(o => o.OrderItems)
                .ToListAsync();
        }
    }
}
