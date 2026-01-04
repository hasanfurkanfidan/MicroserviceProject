namespace MicroserviceProject.Shared.Services
{
    public interface IIdentityService
    {
        Guid GetUserId { get; }
        string UserName { get; }
        List<string> Roles { get; }
    }
}
