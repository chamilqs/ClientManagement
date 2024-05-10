using AutoMapper;
using ClientManagement.Core.Application.Interfaces.Repositories;
using ClientManagement.Core.Application.Interfaces.Services;
using ClientManagement.Core.Application.ViewModels.Address;
using ClientManagement.Core.Domain.Entities;

namespace ClientManagement.Core.Application.Services
{
    public class AddressService : GenericService<SaveAddressViewModel, AddressViewModel, Address>, IAddressService
    {
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;
        public AddressService(IMapper mapper, IAddressRepository addressRepository) : base(addressRepository, mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }
    }
}
