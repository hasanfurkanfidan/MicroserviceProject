using System.ComponentModel.DataAnnotations;

namespace MicroserviceProject.Discount.Api.Options
{
    public class MongoOptions
    {
        [Required]
        public string ConnectionString { get; set; } = default!;

        [Required]
        public string DatabaseName { get; set; } = default!;
    }
}
