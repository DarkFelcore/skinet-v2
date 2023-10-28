namespace SkinetV2.Contracts.Authentication.Common
{
    public record AddressResponse(
        string Street,
        string PostalCode,
        string City,
        string Provice,
        string Country
    );
}