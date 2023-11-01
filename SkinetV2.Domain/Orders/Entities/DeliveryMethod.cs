using SkinetV2.Domain.Orders.Entities.ValueObjects;

namespace SkinetV2.Domain.Orders.Entities
{
    public class DeliveryMethod
    {
        public DeliveryMethodId DeliveryMethodId { get; set; } = null!;
        public string ShortName { get; set; } = string.Empty;
        public string DeliveryTime { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public DeliveryMethod(string shortName, string deliveryTime, string description, decimal price)
        {
            DeliveryMethodId = new DeliveryMethodId(Guid.NewGuid());
            ShortName = shortName;
            DeliveryTime = deliveryTime;
            Description = description;
            Price = price;
        }

        public DeliveryMethod()
        {
        }
    }
}