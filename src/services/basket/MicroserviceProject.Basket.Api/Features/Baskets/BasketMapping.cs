using AutoMapper;
using MicroserviceProject.Basket.Api.Dtos;

namespace MicroserviceProject.Basket.Api.Features.Baskets
{
    public class BasketMapping : Profile
    {
        public BasketMapping()
        {
            CreateMap<BasketDto, Data.Basket>().ReverseMap();
            CreateMap<BasketItemDto, Data.BasketItem>().ReverseMap();

        }
    }
}
