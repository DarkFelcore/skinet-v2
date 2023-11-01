namespace SkinetV2.Contracts.Orders
{
    public record CreateOrderRequest(
        string BasketId,
        string DeliveryMethodId,
        AddressRequest Address
    );
}