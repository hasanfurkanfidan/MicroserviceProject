using MassTransit;
using MediatR;
using MicroserviceProject.Discount.Api.Repositories;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceProject.Discount.Api.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandHandler(AppDbContext context, IIdentityService identityService) : IRequestHandler<CreateDiscountCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var hasCodeForUser = await context.Discounts.AnyAsync(x => x.UserId == identityService.GetUserId && x.Code == request.Code, cancellationToken);
            if (hasCodeForUser)
            {
                return ServiceResult.Error("Discount Code Exists", "You already have a discount with this code.", System.Net.HttpStatusCode.BadRequest);
            }

            var discount = new Discount
            {
                Id = NewId.NextSequentialGuid(),
                Created = DateTime.Now,
                Code = request.Code,
                Rate = request.Rate,
                UserId = identityService.GetUserId,
                Expired = request.Expired
            };

            await context.Discounts.AddAsync(discount, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
