using ClientManagement.Core.Domain.Common;

namespace ClientManagement.Core.Domain.Entities
{
    public class Address : AuditableBaseEntity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public Client? Client { get; set; }
        public int ClientId { get; set; }
    }
}
