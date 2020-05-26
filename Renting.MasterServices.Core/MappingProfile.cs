using AutoMapper;
using Renting.MasterServices.Core.Dtos;
using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Core.Dtos.Provider;
using Renting.MasterServices.Domain.Entities;
using Renting.MasterServices.Domain.Entities.Client;
using Renting.MasterServices.Domain.Entities.Provider;

namespace Renting.MasterServices.Core
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Dummy, DummyDto>();
            CreateMap<DummyDto, Dummy>();

            CreateMap<PlateDto, Plate>();
            CreateMap<Plate, PlateDto>();

            CreateMap<VehicleTypeDto, VehicleType>();
            CreateMap<VehicleType, VehicleTypeDto>();

            CreateMap<ParameterDto, Parameter>();
            CreateMap<Parameter, ParameterDto>();

            CreateMap<EconomicGroupDto, EconomicGroup>();
            CreateMap<EconomicGroup, EconomicGroupDto>();

            CreateMap<ClientUserDto, ClientUser>();
            CreateMap<ClientUser, ClientUserDto>();

            CreateMap<StateDto, State>();
            CreateMap<State, StateDto>();

            CreateMap<AttributeDto, Attribute>();
            CreateMap<Attribute, AttributeDto>();

            CreateMap<PlateKmRequestDto, PlateKmRequest>();
            CreateMap<PlateKmRequest, PlateKmRequestDto>();

            CreateMap<UserProvider, UserProviderDto>();
            CreateMap<UserProviderDto, UserProvider>();

            CreateMap<UserProviderDto, UserSupplierDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.ProviderId))
                .ForMember(dest => dest.EmailUser, opt => opt.MapFrom(src => src.EmailUser));

            CreateMap<Provider, ProviderDto>();
            CreateMap<ProviderDto, Provider>();

            CreateMap<Announcement, AnnouncementDto>();
            CreateMap<AnnouncementDto, Announcement>();
        }
    }
}
