namespace SkinetV2.Contracts.Authentication.Common
{
    public class AddressRequest
    {
        public string? Street { get; set; } = null;
        public string? PostalCode { get; set; } = null;
        public string? City { get; set; } = null;
        public string? Provice { get; set; } = null;
        public string? Country { get; set; } = null;
    }
}