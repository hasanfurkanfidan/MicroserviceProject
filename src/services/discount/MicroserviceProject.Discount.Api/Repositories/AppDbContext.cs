using MicroserviceProject.Discount.Api.Features.Discounts;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace MicroserviceProject.Discount.Api.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Api.Features.Discounts.Discount> Discounts { get; set; }
        public static AppDbContext Create(IMongoDatabase database)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName);
            var appDbContext = new AppDbContext(optionsBuilder.Options);

            return appDbContext;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
