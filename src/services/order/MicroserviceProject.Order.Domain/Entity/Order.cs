using MassTransit;
using System.Text;

namespace MicroserviceProject.Order.Domain.Entity
{
    public class Order : BaseEntity<Guid>
    {
        public string Code { get; set; } = default!;
        public DateTime Created { get; set; }
        public Guid BuyerId { get; set; }
        public OrderStatus Status { get; set; }
        public int AddressId { get; set; }
        public decimal TotalPrice { get; set; }
        public float? DiscountRate { get; set; }
        public Guid? PaymentId { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new();
        public Address Address { get; set; } = default!;

        #region Behavior Methods
        public static string GenerateCode()
        {
            var random = new Random();
            var orderCode = new StringBuilder(10);

            for (int i = 0; i < 10; i++)
            {
                orderCode.Append(random.Next(0, 10));
            }

            return orderCode.ToString();
        }

        public static Order CreateUnPaidOrder(Guid buyerId, float? discountRate, int addressId)
        {
            return new Order
            {
                Id = NewId.NextGuid(),
                Code = GenerateCode(),
                AddressId = addressId,
                BuyerId = buyerId,
                Created = DateTime.Now,
                Status = OrderStatus.WaitingForPayment,
                TotalPrice = 0,
                DiscountRate = discountRate
            };
        }

        public void AddOrderItem(Guid productId, string productName, decimal unitPrice)
        {
            var orderItem = new OrderItem();
            orderItem.SetItem(productId, productName, unitPrice);
            OrderItems.Add(orderItem);

            CalculateTotalPrice();
        }

        public void ApplyDiscount(float discountPercentage)
        {
            if (discountPercentage < 0 || discountPercentage > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount rate must be between 0 and 100.");
            }
            DiscountRate = discountPercentage;
            CalculateTotalPrice();
        }

        public void SetPaidStatus(Guid paymentId)
        {
            Status = OrderStatus.Paid;
            PaymentId = paymentId;
        }

        private void CalculateTotalPrice()
        {
            decimal total = 0;
            foreach (var item in OrderItems)
            {
                total += item.UnitPrice;
            }

            if (DiscountRate.HasValue)
            {
                total = total - (total * ((decimal)DiscountRate.Value / 100));
            }

            TotalPrice = total;
        }

        #endregion
    }

}
