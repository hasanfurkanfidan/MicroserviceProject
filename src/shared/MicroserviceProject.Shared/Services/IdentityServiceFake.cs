
namespace MicroserviceProject.Shared.Services
{
    public class IdentityServiceFake : IIdentityService
    {
        public Guid GetUserId => Guid.Parse("55285f69-d8a6-451a-9a7a-056fb8194f82");
        public string UserName => "hasanfurkanfidan";
    }
}
