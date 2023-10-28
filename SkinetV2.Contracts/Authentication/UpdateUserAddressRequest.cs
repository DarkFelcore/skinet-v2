namespace SkinetV2.Contracts.Authentication
{
    public record UpdateUserAddressRequest(
        string Street,
        string PostalCode,
        string Provice,
        string City,
        string Country
    );
}