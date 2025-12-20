namespace MicroserviceProject.Order.Domain.Entity
{
    public class BaseEntity<TEntityId>
    {
        public TEntityId Id { get; set; } = default!;
    }
}
