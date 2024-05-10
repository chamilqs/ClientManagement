using AutoMapper;
using ClientManagement.Core.Application.Dtos.Account;
using ClientManagement.Core.Application.ViewModels.Address;
using ClientManagement.Core.Application.ViewModels.Client;
using ClientManagement.Core.Application.ViewModels.User;
using ClientManagement.Core.Domain.Entities;

namespace ClientManagement.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {

            #region UserProfile
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateUserRequest, SaveUserViewModel>()
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                .ForMember(dest => dest.Error, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region ClientProfile
            CreateMap<Client, SaveClientViewModel>()
                .ForMember(x => x.Street, opt => opt.Ignore())
                .ForMember(x => x.State, opt => opt.Ignore())
                .ForMember(x => x.City, opt => opt.Ignore())
                .ForMember(x => x.PostalCode, opt => opt.Ignore())
                .ForMember(x => x.Country, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Addresses, opt => opt.Ignore());

            CreateMap<Client, ClientViewModel>()
                .ForMember(x => x.Addresses, opt => opt.Ignore())
                .ForMember(x => x.CompleteName, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Addresses, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region AddressProfile
            CreateMap<Address, SaveAddressViewModel>()
                .ReverseMap()
                .ForMember(x => x.Client, opt => opt.Ignore());

            CreateMap<Address, AddressViewModel>()
                .ReverseMap()
                .ForMember(x => x.Client, opt => opt.Ignore());

            #endregion

        }
    }
}
