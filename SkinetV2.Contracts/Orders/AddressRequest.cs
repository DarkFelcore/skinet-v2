using System.ComponentModel.DataAnnotations;

namespace SkinetV2.Contracts.Orders
{
    public class AddressRequest
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Street { get; set; } = string.Empty;
        [Required]
        public string PostalCode { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Provice { get; set; } = string.Empty;
        [Required]
        public string Country { get; set; } = string.Empty;
    }
}