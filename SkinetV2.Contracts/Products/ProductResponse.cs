namespace SkinetV2.Contracts.Products
{
    public record ProductResponse(
        Guid ProductId,
        string Name,
        string Description,
        decimal Price,
        string PictureUrl,
        string ProductBrandName,
        string ProductTypeName
    );
}