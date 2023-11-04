using SkinetV2.Domain.Orders.Entities;

namespace SkinetV2.Application.common.Specifications
{
    public class DeliveryMethodsWithPriceDescSpecification : BaseSpecification<DeliveryMethod>
    {
        public DeliveryMethodsWithPriceDescSpecification() 
            : base()
        {
            AddOrderByDescending(x => x.Price);
        }
    }
}