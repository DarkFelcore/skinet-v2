namespace SkinetV2.Application.Authentication.Common
{
    public record AddressResult(
        string Street,
        string PostalCode,
        string Provice,
        string City,
        string Country
    );
}