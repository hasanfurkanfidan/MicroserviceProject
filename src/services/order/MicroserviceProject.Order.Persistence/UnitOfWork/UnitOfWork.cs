using MicroserviceProject.Order.Application.Contracts.UnitOfWorks;

namespace MicroserviceProject.Order.Persistence.UnitOfWork
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            context.Database.BeginTransaction(cancellationToken);
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
