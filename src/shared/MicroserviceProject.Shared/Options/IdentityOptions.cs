namespace MicroserviceProject.Shared.Options
{
    public class IdentityOptions
    {
        public required string Issuer { get; set; }
        public required string Address { get; set; }
        public required string Audience { get; set; }
    }
}
