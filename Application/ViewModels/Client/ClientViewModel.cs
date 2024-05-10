using ClientManagement.Core.Application.ViewModels.Address;

namespace ClientManagement.Core.Application.ViewModels.Client
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompleteName => $"{FirstName} {LastName}";
        public string IdentificationNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<AddressViewModel> Addresses { get; set; }
    }
}
