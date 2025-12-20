using MicroserviceProject.Order.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroserviceProject.Order.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(builder => builder.Id);
            builder.Property(builder => builder.Id).UseIdentityColumn();
            builder.Property(builder => builder.Province).IsRequired().HasMaxLength(100);   
            builder.Property(builder => builder.District).IsRequired().HasMaxLength(100);
            builder.Property(builder => builder.Street).IsRequired().HasMaxLength(200);
            builder.Property(builder => builder.Line).IsRequired().HasMaxLength(300);
            builder.Property(builder => builder.ZipCode).IsRequired().HasMaxLength(18);
        }
    }
}
