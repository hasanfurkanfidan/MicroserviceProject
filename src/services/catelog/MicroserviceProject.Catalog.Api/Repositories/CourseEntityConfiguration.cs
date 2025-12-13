using MicroserviceProject.Catalog.Api.Features.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection.Emit;

namespace MicroserviceProject.Catalog.Api.Repositories
{
    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToCollection("courses");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.Name).HasElementName("name").HasMaxLength(100);
            builder.Property(p => p.Description).HasElementName("description").HasMaxLength(1000);
            builder.Property(p => p.ImageUrl).HasElementName("imageUrl").HasMaxLength(200);
            builder.Property(p => p.Created).HasElementName("created");
            builder.Property(p => p.Price).HasElementName("price");
            builder.Property(p => p.UserId).HasElementName("userId");
            builder.Property(p => p.Category).HasElementName("categoryId");
            builder.Ignore(p => p.Category);

            builder.OwnsOne(p => p.Feature, feature =>
            {
                feature.HasElementName("feature");
                feature.Property(f => f.Duration).HasElementName("duration");
                feature.Property(f => f.Rating).HasElementName("rating");
                feature.Property(f => f.EducatorFullName).HasElementName("educatorFullName").HasMaxLength(200);
            });
        }
    }
}
