using AutoMapper;
using MicroserviceProject.Discount.Api.Features.Discounts.CreateDiscount;

namespace MicroserviceProject.Discount.Api.Features.Discounts
{
    public class DiscountMapping : Profile
    {
        public DiscountMapping()
        {
            CreateMap<CreateDiscountCommand, Discount>();
        }
    }
}
