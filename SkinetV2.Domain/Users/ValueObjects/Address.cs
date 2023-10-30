namespace SkinetV2.Domain.Users.ValueObjects
{
    public class Address
    {
        public string? Street { get; set; } = null;

        public string? PostalCode { get; set; } = null;

        public string? Provice { get; set; } = null;

        public string? City { get; set; } = null;

        public string? Country { get; set; } = null;

        public Address(string? street = null, string? postalCode = null, string? provice = null, string? city = null, string? country = null)
        {
            Street = street;
            PostalCode = postalCode;
            Provice = provice;
            City = city;
            Country = country;
        }
    }
}