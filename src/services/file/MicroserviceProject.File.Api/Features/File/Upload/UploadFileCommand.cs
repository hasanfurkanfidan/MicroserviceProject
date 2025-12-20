using MicroserviceProject.Shared;

namespace MicroserviceProject.File.Api.Features.File.Upload
{
    public record UploadFileCommand(IFormFile File) : IRequestByServiceResult<UploadFileResponse>;
}
