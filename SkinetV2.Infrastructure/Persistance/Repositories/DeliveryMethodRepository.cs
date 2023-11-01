using Microsoft.EntityFrameworkCore;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Orders.Entities;
using SkinetV2.Domain.Orders.Entities.ValueObjects;

namespace SkinetV2.Infrastructure.Persistance.Repositories
{
    public class DeliveryMethodRepository : GenericRepository<DeliveryMethod>, IDeliveryMethodRepository
    {
        public DeliveryMethodRepository(StoreContext context) : base(context)
        {
        }

        public override async Task<DeliveryMethod?> GetByIdAsync(Guid id)
        {
            var deliveryMethodId = new DeliveryMethodId(id);
            return await _context.DeliveryMethods.FirstOrDefaultAsync(x => x.DeliveryMethodId == deliveryMethodId);
        }
    }
}