using ClientManagement.Core.Application.ViewModels.Address;
using System.ComponentModel.DataAnnotations;

namespace ClientManagement.Core.Application.ViewModels.Client
{
    public class SaveClientViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string IdentificationNumber { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Street { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string City { get; set; }
        public string? State { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Country { get; set; }
        public List<AddressViewModel>? Addresses { get; set; }
    }
}
