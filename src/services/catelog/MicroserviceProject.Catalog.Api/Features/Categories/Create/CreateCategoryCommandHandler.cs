using MassTransit;
using MediatR;
using MicroserviceProject.Catalog.Api.Repositories;
using MicroserviceProject.Shared;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MicroserviceProject.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existCategory = await context.Categories
                .AnyAsync(c => c.Name == request.Name, cancellationToken);

            if (existCategory)
                return ServiceResult<CreateCategoryResponse>.Error("Category name already exist", $"CategoryName {request.Name} already exist.", HttpStatusCode.BadRequest);

            var category = new Category
            {
                Name = request.Name,
                Id = NewId.NextSequentialGuid()
            };

            await context.AddAsync(category, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            var response = new CreateCategoryResponse(category.Id);

            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(response, $"/categories/{category.Id}");
        }
    }
}
