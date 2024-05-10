using ClientManagement.Core.Application.ViewModels.Address;
using ClientManagement.Core.Application.ViewModels.Client;
using ClientManagement.Core.Domain.Entities;

namespace ClientManagement.Core.Application.Interfaces.Services
{
    public interface IClientService : IGenericService<SaveClientViewModel, ClientViewModel, Client>
    {
        Task<List<ClientViewModel>> GetAllViewModelWithInclude();
        Task<SaveAddressViewModel> AddAddress(int id, Address address);
    }
}
