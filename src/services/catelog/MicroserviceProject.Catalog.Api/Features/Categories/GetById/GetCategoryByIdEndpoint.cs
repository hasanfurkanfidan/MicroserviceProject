
using MicroserviceProject.Catalog.Api.Features.Categories.Dtos;
using MicroserviceProject.Catalog.Api.Repositories;


namespace MicroserviceProject.Catalog.Api.Features.Categories.GetById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequestByServiceResult<CategoryDto>;
    public class GetCategoryByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
    {
        public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await context.Categories.FindAsync(request.Id, cancellationToken);
            if (result == null)
                return ServiceResult<CategoryDto>.Error("Not Found", $"Category with Id {request.Id} was not found.", System.Net.HttpStatusCode.NotFound);

            var dto = mapper.Map<CategoryDto>(result);

            return ServiceResult<CategoryDto>.SuccessAsOk(dto);
        }
    }
    public static class GetCategoryByIdEndpoint
    {
        public static RouteGroupBuilder GetByIdCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetCategoryByIdQuery(id));

                return result.ToGenericResult();
            });

            return group;
        }
    }
}
