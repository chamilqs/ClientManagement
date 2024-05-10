namespace ClientManagement.Core.Application.ViewModels.Address
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

    }
}
