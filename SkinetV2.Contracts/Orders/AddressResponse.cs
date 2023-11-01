namespace SkinetV2.Contracts.Orders
{
    public record AddressResponse(
        string FirstName,
        string LastName,
        string Street,
        string PostalCode,
        string Provice,
        string City,
        string Country
    );
}