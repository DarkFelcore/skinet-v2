namespace SkinetV2.Contracts.Orders
{
    public record OrderReponse(
        Guid OrderId,
        List<OrderItemResponse> OrderItems,
        string BuyerEmail,
        DateTimeOffset OrderDate,
        AddressResponse ShipToAddress,
        DeliveryMethodResponse DeliveryMethod,
        decimal Subtotal,
        decimal Total, // This property gets automatically bounded to the GetTotal method in the Order.cs entity class. Get + Total = automatic bound.
        string Status,
        string PaymentIntentId
    );
}