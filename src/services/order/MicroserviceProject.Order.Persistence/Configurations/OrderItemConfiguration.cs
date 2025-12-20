using MicroserviceProject.Order.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroserviceProject.Order.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(builder => builder.Id);
            builder.Property(builder => builder.ProductId).IsRequired();
            builder.Property(builder => builder.ProductName).IsRequired().HasMaxLength(200);
            builder.Property(builder => builder.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
        }
    }
}
