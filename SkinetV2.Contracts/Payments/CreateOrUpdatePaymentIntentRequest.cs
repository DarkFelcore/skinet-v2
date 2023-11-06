namespace SkinetV2.Contracts.Payments
{
    public record CreateOrUpdatePaymentIntentRequest(
        string BasketId
    );
}