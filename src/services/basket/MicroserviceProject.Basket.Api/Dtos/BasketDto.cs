using System.Text.Json.Serialization;

namespace MicroserviceProject.Basket.Api.Dtos
{
    public record BasketDto
    {
        public BasketDto(Guid userId, List<BasketItemDto> items)
        {
            Items = items;
            UserId = userId;
        }

        public BasketDto()
        {
            
        }

        [JsonIgnore]
        public Guid UserId { get; init; }

        public List<BasketItemDto> Items { get; set; } = new();
    }
}
