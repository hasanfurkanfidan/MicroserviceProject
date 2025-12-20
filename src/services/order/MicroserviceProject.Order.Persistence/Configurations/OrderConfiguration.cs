using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroserviceProject.Order.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Entity.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entity.Order> builder)
        {
            builder.HasKey(builder => builder.Id);
            builder.Property(builder => builder.Id).ValueGeneratedNever();

            builder.Property(builder => builder.Code).IsRequired().HasMaxLength(50);
            builder.Property(builder => builder.Created).IsRequired();
            builder.Property(builder => builder.BuyerId).IsRequired();
            builder.Property(builder => builder.Status).IsRequired();
            builder.Property(builder => builder.AddressId).IsRequired();
            builder.Property(builder => builder.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(builder => builder.DiscountRate).IsRequired(false);

            builder.HasMany(builder => builder.OrderItems)
                   .WithOne()
                   .HasForeignKey(orderItem => orderItem.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(builder => builder.Address)
                   .WithMany()
                   .HasForeignKey(order => order.AddressId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
