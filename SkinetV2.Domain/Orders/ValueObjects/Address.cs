namespace SkinetV2.Domain.Orders.ValueObjects
{
    public class Address
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Provice { get; set; }= string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public Address(string firstName, string lastName, string street, string postalCode, string province, string city, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            PostalCode = postalCode;
            Provice = province;
            City = city;
            Country = country;
        }
        public Address()
        {
        }
    }
}