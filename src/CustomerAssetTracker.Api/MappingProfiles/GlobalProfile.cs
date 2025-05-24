    using AutoMapper;
    using CustomerAssetTracker.Core;
    using CustomerAssetTracker.Api.DTOs;

namespace CustomerAssetTracker.Api.MappingProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            // Mapping from Customer (entity) to CustomerDto.
            // Machine and License counts are calculated from the collections in the Customer entity.
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.MachineCount, opt => opt.MapFrom(src => src.Machines.Count))
                .ForMember(dest => dest.LicenseCount, opt => opt.MapFrom(src => src.Licenses.Count));

            // Mapping from CreateCustomerDto to Customer (entity).
            CreateMap<CreateCustomerDto, Customer>();

            // Mapping from UpdateCustomerDto to Customer (entity).
            CreateMap<UpdateCustomerDto, Customer>();
        }
    }

    public class MachineProfile : Profile
    {
        public MachineProfile()
        {
            // Mapping from Machine (entity) to MachineDto.
            // 
            CreateMap<Machine, MachineDto>()
                .ForMember(dest => dest.MachineType, opt => opt.MapFrom(src => src.MachineType.ToString()))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Name : string.Empty))
                .ForMember(dest => dest.LicenseCount, opt => opt.MapFrom(src => src.Licenses != null ? src.Licenses.Count : 0))
                .ForMember(dest => dest.ServiceRecordCount, opt => opt.MapFrom(src => src.ServiceRecords != null ? src.ServiceRecords.Count : 0));

            // Mapping from CreateMachineDto and UpdateMachineDto to Machine (entity).
            CreateMap<CreateMachineDto, Machine>()
                .ForMember(dest => dest.MachineType, opt => opt.MapFrom(src => Enum.Parse<Machine.MachineTypes>(src.MachineType)));
            CreateMap<UpdateMachineDto, Machine>()
                .ForMember(dest => dest.MachineType, opt => opt.MapFrom(src => Enum.Parse<Machine.MachineTypes>(src.MachineType)));
        }
    }
    
    public class LicenseProfile : Profile
    {
        public LicenseProfile()
        {
            // Mapping from License (entity) to LicenseDto.
            CreateMap<License, LicenseDto>()
                .ForMember(dest => dest.LicenseType, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.MachineName, opt => opt.MapFrom(src => src.Machine != null ? src.Machine.Name : null))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Name : string.Empty));

            // Mapping from CreateLicenseDto and UpdateLicenseDto to License (entity).
            CreateMap<CreateLicenseDto, License>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<License.LicenseType>(src.LicenseType)));
            CreateMap<UpdateLicenseDto, License>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<License.LicenseType>(src.LicenseType)));
        }
    }

    public class ServiceRecordProfile : Profile
    {
        public ServiceRecordProfile()
        {
            // Mapping from ServiceRecord (entity) to ServiceRecordDto.
            CreateMap<ServiceRecord, ServiceRecordDto>()
                .ForMember(dest => dest.MachineName, opt => opt.MapFrom(src => src.Machine != null ? src.Machine.Name : string.Empty));

            // Mapping from CreateServiceRecordDto and UpdateServiceRecordDto to ServiceRecord (entity).
            CreateMap<CreateServiceRecordDto, ServiceRecord>();
            CreateMap<UpdateServiceRecordDto, ServiceRecord>();
        }
    }
}
    