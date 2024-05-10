using ClientManagement.Core.Domain.Common;

namespace ClientManagement.Core.Domain.Entities
{
    public class Client : AuditableBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentificationNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
