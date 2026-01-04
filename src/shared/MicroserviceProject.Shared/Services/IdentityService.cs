
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MicroserviceProject.Shared.Services
{
    public class IdentityService(IHttpContextAccessor httpContextAccessor) : IIdentityService
    {
        public Guid GetUserId
        {
            get
            {
                if (!httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
                {
                    throw new UnauthorizedAccessException("User is not authenticated!");
                }

                return Guid.Parse(httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(p=>p.Type == ClaimTypes.NameIdentifier)!.Value);
            }
        }
        public string UserName
        {
            get
            {
                if (!httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
                {
                    throw new UnauthorizedAccessException("User is not authenticated!");
                }

                return httpContextAccessor.HttpContext!.User.Identity!.Name!;
            }
        }

        public List<string> Roles 
        {
            get
            {
                if (!httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
                {
                    throw new UnauthorizedAccessException("User is not authenticated!");
                }

                return httpContextAccessor.HttpContext!.User.Claims.Where(p => p.Type == ClaimTypes.Role).Select(p => p.Value).ToList();
            }
        }
    }
}
