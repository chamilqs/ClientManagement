using System.ComponentModel.DataAnnotations;

namespace ClientManagement.Core.Application.ViewModels.Address
{
    public class SaveAddressViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Street { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string City { get; set; }
        public string? State { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Country { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int ClientId { get; set; }
    }
}
