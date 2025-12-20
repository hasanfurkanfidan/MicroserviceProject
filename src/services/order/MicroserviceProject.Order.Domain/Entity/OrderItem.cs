namespace MicroserviceProject.Order.Domain.Entity
{
    //Rich Domain Model
    public class OrderItem : BaseEntity<int>
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public Guid OrderId { get; set; } = default!;
        public Order Order { get; set; } = default!;

        #region Behavior Methods
        public void SetItem(Guid productId, string productName, decimal unitPrice)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentNullException("Product name cannot be null or empty.", nameof(productName));

            if (unitPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price cannot be negative.");


            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
        }

        public void UpdatePrice(decimal newUnitPrice)
        {
            if (newUnitPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(newUnitPrice), "Unit price cannot be negative.");

            UnitPrice = newUnitPrice;
        }

        public void ApplyDiscount(float discountPercentage)
        {
            if (discountPercentage < 0 || discountPercentage > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount percentage must be between 0 and 100.");
            }

            UnitPrice = UnitPrice - (UnitPrice * ((decimal)discountPercentage / 100));
        }

        public bool IsSameItem(OrderItem orderItem)
        {
            return ProductId == orderItem.ProductId;
        }
        #endregion
    }
}
