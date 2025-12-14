using System.Text.Json.Serialization;

namespace MicroserviceProject.Basket.Api.Dtos
{
    public record BasketDto
    {
        public BasketDto(List<BasketItemDto> items)
        {
            Items = items;
        }

        public BasketDto()
        {
            
        }

        public List<BasketItemDto> Items { get; set; } = new();

        public decimal TotalPrice => Items.Sum(item => item.Price);
        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }

        [JsonIgnore]
        public bool IsApplyDiscount => DiscountRate > 0 && !string.IsNullOrEmpty(Coupon);

        public decimal? TotalPriceWithAppliedDiscount
        {
            get
            {
                if (!IsApplyDiscount)
                {
                    return null;
                }

                return Items.Sum(x => x.PriceByApplyDiscountRate);
            }
        }
    }
}
