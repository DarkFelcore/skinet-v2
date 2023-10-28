namespace SkinetV2.Application.common.Interfaces
{
    public interface IUnitOfWork
    {
        // Add Repository Interfaces here
        IProductRepository ProductRepository { get; }
        IUserRepository UserRepository { get; }
        Task CompleteAsync();
    }
}