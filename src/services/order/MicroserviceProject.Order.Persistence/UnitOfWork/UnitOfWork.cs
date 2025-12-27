using MicroserviceProject.Order.Application.Contracts.UnitOfWorks;

namespace MicroserviceProject.Order.Persistence.UnitOfWork
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
           await context.Database.BeginTransactionAsync(cancellationToken);
        }

        public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            return context.Database.CommitTransactionAsync(cancellationToken);
        }

        public Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return context.SaveChangesAsync(cancellationToken);
        }
    }
}
