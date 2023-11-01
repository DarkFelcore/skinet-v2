namespace SkinetV2.Contracts.Orders
{
    public record DeliveryMethodResponse(
        Guid DeliveryMethodId,
        string ShortName,
        string DeliveryTime,
        string Description,
        decimal Price
    );
}