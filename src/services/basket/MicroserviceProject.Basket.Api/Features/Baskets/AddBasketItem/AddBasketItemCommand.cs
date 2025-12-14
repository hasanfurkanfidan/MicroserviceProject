using MicroserviceProject.Shared;

namespace MicroserviceProject.Basket.Api.Features.Baskets.AddBasketItem
{
    public record AddBasketItemCommand(Guid CourseId,string CourseName,decimal CoursePrice,string ImageUrl) : IRequestByServiceResult;
}
