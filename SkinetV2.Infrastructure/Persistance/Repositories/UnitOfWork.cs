using SkinetV2.Application.common.Interfaces;

namespace SkinetV2.Infrastructure.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly StoreContext _context;

        public IProductRepository ProductRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public IDeliveryMethodRepository DeliveryMethodRepository { get; private set; }

        public UnitOfWork(StoreContext context)
        {
            _context = context;

            // Repositories
            ProductRepository = new ProductRepository(_context);
            UserRepository = new UserRepository(_context);
            OrderRepository = new OrderRepository(_context);
            DeliveryMethodRepository = new DeliveryMethodRepository(_context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}