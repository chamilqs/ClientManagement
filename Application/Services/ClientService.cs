using AutoMapper;
using ClientManagement.Core.Application.Interfaces.Repositories;
using ClientManagement.Core.Application.Interfaces.Services;
using ClientManagement.Core.Application.ViewModels.Address;
using ClientManagement.Core.Application.ViewModels.Client;
using ClientManagement.Core.Domain.Entities;

namespace ClientManagement.Core.Application.Services
{
    public class ClientService : GenericService<SaveClientViewModel, ClientViewModel, Client>, IClientService
    {
        private readonly IMapper _mapper;
        private readonly IClientRepository _clientRepository;
        private readonly IAddressService _addressService;
        public ClientService(IMapper mapper, IClientRepository clientRepository, IAddressService addressService) : base(clientRepository, mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _addressService = addressService;
        }

        public override async Task<SaveClientViewModel> Add(SaveClientViewModel vm)
        {
            try
            {
                var client = await base.Add(vm);

                if (vm != null)
                {
                    SaveAddressViewModel saveAddressViewModel = new SaveAddressViewModel
                    {
                        ClientId = client.Id,
                        City = vm.City,
                        Street = vm.Street,
                        State = vm.State,
                        Country = vm.Country,
                        PostalCode = vm.PostalCode
                    };

                    var address = await _addressService.Add(saveAddressViewModel);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return vm;

        }

        public async Task<SaveAddressViewModel> AddAddress(int ClientId, Address address)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(ClientId);
                if (client != null)
                {
                    SaveAddressViewModel saveAddressViewModel = new SaveAddressViewModel
                    {
                        ClientId = client.Id,
                        City = address.City,
                        Street = address.Street,
                        State = address.State,
                        Country = address.Country,
                        PostalCode = address.PostalCode
                    };

                    var newAddress = await _addressService.Add(saveAddressViewModel);

                    return saveAddressViewModel;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }

            return null;

        }

        public async Task<List<ClientViewModel>> GetAllViewModelWithInclude()
        {
            var clientList = await _clientRepository.GetAllWithIncludeAsync(new List<string> { "Addresses" });

            return clientList.Select(client => new ClientViewModel
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                IdentificationNumber = client.IdentificationNumber,
                Phone = client.Phone,
                Addresses = _mapper.Map<List<AddressViewModel>>(client.Addresses.Where(x => x.ClientId == client.Id).ToList())

            }).ToList();

        }



    }
}

