using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace MicroserviceProject.Discount.Api.Repositories
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Features.Discounts.Discount>
    {
        public void Configure(EntityTypeBuilder<Features.Discounts.Discount> builder)
        {
            builder.ToCollection("discounts");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();

            builder.Property(p => p.Code).HasElementName("code").HasMaxLength(150);
            builder.Property(p => p.Created).HasElementName("created");
            builder.Property(p => p.Rate).HasElementName("rate");

            builder.Property(p => p.UserId).HasElementName("user_id");
            builder.Property(p => p.Updated).HasElementName("updated");
            builder.Property(p => p.Expired).HasElementName("expired");
        }
    }
}
