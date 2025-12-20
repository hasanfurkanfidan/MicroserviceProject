using MicroserviceProject.Order.Domain.Entity;
using System.Linq.Expressions;

namespace MicroserviceProject.Order.Application.Contracts.Repositories
{
    public interface IGenericRepository<TId, TEntity> where TId : struct where TEntity : BaseEntity<TId>
    {
        // Read
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllPagedAsync(int pageNumber, int pageSize);
        ValueTask<TEntity> GetByIdAsync(TId id);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>>? predicate = null);
        // Existence
        Task<bool> AnyAsync(TId id);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        // Write
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
