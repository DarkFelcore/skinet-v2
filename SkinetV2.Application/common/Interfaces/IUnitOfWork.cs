namespace SkinetV2.Application.common.Interfaces
{
    public interface IUnitOfWork
    {
        // Add Repository Interfaces here
        IProductRepository ProductRepository { get; }
        Task CompleteAsync();
    }
}