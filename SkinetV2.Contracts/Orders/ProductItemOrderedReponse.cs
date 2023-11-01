namespace SkinetV2.Contracts.Orders
{
    public record ProductItemOrderedReponse(
        Guid ProductItemId,
        string ProductName,
        string PictureUrl
    );
}