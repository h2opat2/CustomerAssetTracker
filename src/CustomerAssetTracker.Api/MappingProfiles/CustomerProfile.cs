    using AutoMapper; // Důležité pro Profile
    using CustomerAssetTracker.Core; // Pro doménové entity
    using CustomerAssetTracker.Api.DTOs; // Pro DTOs

namespace CustomerAssetTracker.Api.MappingProfiles
{
    // Komentář: Třída CustomerProfile dědí z AutoMapper.Profile.
    // Zde definujeme pravidla mapování mezi entitami a DTOs.
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            // Komentář: Mapování z Customer (entity) na CustomerDto.
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.MachineCount, opt => opt.MapFrom(src => src.Machines.Count))
                .ForMember(dest => dest.LicenseCount, opt => opt.MapFrom(src => src.Licenses.Count));

            // Komentář: Mapování z CreateCustomerDto na Customer (entitu).
            CreateMap<CreateCustomerDto, Customer>();

            // Komentář: Mapování z UpdateCustomerDto na Customer (entitu).
            CreateMap<UpdateCustomerDto, Customer>();
        }
    }

    public class MachineProfile : Profile
    {
        public MachineProfile()
        {
            // Mapování z Machine (entity) na MachineDto.
            // Zde mapujeme enum MachineType na string a počty kolekcí.
            // Důležité: CustomerName a MachineName budou null/prázdné, pokud nejsou související entity načteny (Eager Loading).
            // Pro správné načtení CustomerName/MachineName by bylo potřeba rozšířit GenericRepository
            // nebo vytvořit specifické repozitáře s metodami pro .Include().
            CreateMap<Machine, MachineDto>()
                .ForMember(dest => dest.MachineType, opt => opt.MapFrom(src => src.MachineType.ToString()))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Name : string.Empty))
                .ForMember(dest => dest.LicenseCount, opt => opt.MapFrom(src => src.Licenses != null ? src.Licenses.Count : 0))
                .ForMember(dest => dest.ServiceRecordCount, opt => opt.MapFrom(src => src.ServiceRecords != null ? src.ServiceRecords.Count : 0));

            // Mapování z DTO na Machine entitu.
            // Pro MachineType mapujeme string na enum.
            CreateMap<CreateMachineDto, Machine>()
                .ForMember(dest => dest.MachineType, opt => opt.MapFrom(src => Enum.Parse<Machine.MachineTypes>(src.MachineType)));
            CreateMap<UpdateMachineDto, Machine>()
                .ForMember(dest => dest.MachineType, opt => opt.MapFrom(src => Enum.Parse<Machine.MachineTypes>(src.MachineType)));
        }
    }
    
    // Komentář: Mapovací profil pro entitu License
    public class LicenseProfile : Profile
    {
        public LicenseProfile()
        {
            // Mapování z License (entity) na LicenseDto.
            CreateMap<License, LicenseDto>()
                .ForMember(dest => dest.LicenseType, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.MachineName, opt => opt.MapFrom(src => src.Machine != null ? src.Machine.Name : null))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Name : string.Empty));

            // Mapování z DTO na License entitu.
            CreateMap<CreateLicenseDto, License>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<License.LicenseType>(src.LicenseType)));
            CreateMap<UpdateLicenseDto, License>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<License.LicenseType>(src.LicenseType)));
        }
    }

    // Komentář: Mapovací profil pro entitu ServiceRecord
    public class ServiceRecordProfile : Profile
    {
        public ServiceRecordProfile()
        {
            // Mapování z ServiceRecord (entity) na ServiceRecordDto.
            CreateMap<ServiceRecord, ServiceRecordDto>()
                .ForMember(dest => dest.MachineName, opt => opt.MapFrom(src => src.Machine != null ? src.Machine.Name : string.Empty));

            // Mapování z DTO na ServiceRecord entitu.
            CreateMap<CreateServiceRecordDto, ServiceRecord>();
            CreateMap<UpdateServiceRecordDto, ServiceRecord>();
        }
    }
}
    