namespace SkinetV2.Contracts.Orders
{
    public record OrderItemResponse(
        Guid OrderItemId,
        ProductItemOrderedReponse ItemOrdered,
        decimal Price,
        int Quantity
    );
}