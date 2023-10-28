namespace SkinetV2.Contracts.Authentication.Common
{
    public class AddressRequest
    {
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Provice { get; set; }
        public string Country { get; set; }
    }
}