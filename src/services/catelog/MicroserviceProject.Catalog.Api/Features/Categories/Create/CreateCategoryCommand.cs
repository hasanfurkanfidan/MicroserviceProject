using MediatR;
using MicroserviceProject.Shared;

namespace MicroserviceProject.Catalog.Api.Features.Categories.Create
{
    public record CreateCategoryCommand(string Name) : IRequest<ServiceResult<CreateCategoryResponse>>;
}
