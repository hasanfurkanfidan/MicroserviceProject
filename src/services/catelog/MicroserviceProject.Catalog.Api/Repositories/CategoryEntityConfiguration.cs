using MicroserviceProject.Catalog.Api.Features.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace MicroserviceProject.Catalog.Api.Repositories
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToCollection("categories");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.Name).HasElementName("name").HasMaxLength(100);
            builder.Ignore(p => p.Courses);
        }    
    }
}
