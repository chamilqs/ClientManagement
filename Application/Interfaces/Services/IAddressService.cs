using ClientManagement.Core.Application.ViewModels.Address;
using ClientManagement.Core.Domain.Entities;

namespace ClientManagement.Core.Application.Interfaces.Services
{
    public interface IAddressService : IGenericService<SaveAddressViewModel, AddressViewModel, Address>
    {

    }
}
