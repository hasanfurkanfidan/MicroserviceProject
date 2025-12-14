namespace MicroserviceProject.Basket.Api.Data
{
    public class Basket
    {
        public Guid UserId { get; set; }
        public List<BasketItem> Items { get; set; } = new();
        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }

        public decimal TotalPrice => Items.Sum(item => item.Price);
        public bool IsApplyDiscount => DiscountRate > 0 && !string.IsNullOrEmpty(Coupon);

        public Basket(Guid userId, List<BasketItem> items)
        {
            UserId = userId;
            Items = items;
        }
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


        public void ApplyNewDiscount(string coupon, float discountRate)
        {
            Coupon = coupon;
            DiscountRate = discountRate;
            foreach (var item in Items)
            {
                item.PriceByApplyDiscountRate = item.Price - (decimal)(1 - discountRate);
            }
        }

        public void ApplyAvailableDiscount()
        {
            foreach (var item in Items)
            {
                item.PriceByApplyDiscountRate = item.Price - (decimal)(1 - DiscountRate!);
            }
        }

        public void ClearDiscount()
        {
            Coupon = null;
            DiscountRate = null;
            foreach (var item in Items)
            {
                item.PriceByApplyDiscountRate = null;
            }
        }
    }
}
