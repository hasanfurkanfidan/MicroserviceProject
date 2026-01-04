using Asp.Versioning.Builder;
using MicroserviceProject.File.Api.Features.File.Delete;
using MicroserviceProject.File.Api.Features.File.Upload;

namespace MicroserviceProject.File.Api.Features.Discounts
{
    public static class FileEndpointExt
    {
        public static void AddFileGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v:{version:apiversion}/files").WithTags("Files").WithApiVersionSet(apiVersionSet).UploadFileGroupItemEndpoint().DeleteFileGroupItemEndpoint().RequireAuthorization("Password");
        }
    }
}
