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
                // AutoMapper automaticky mapuje vlastnosti se stejnými názvy.
                // Pro MachineCount a LicenseCount musíme definovat explicitní mapování,
                // protože tyto vlastnosti nejsou přímo na Customer entitě, ale jsou odvozené.
                CreateMap<Customer, CustomerDto>()
                    .ForMember(dest => dest.MachineCount, opt => opt.MapFrom(src => src.Machines.Count))
                    .ForMember(dest => dest.LicenseCount, opt => opt.MapFrom(src => src.Licenses.Count));

                // Komentář: Mapování z CreateCustomerDto na Customer (entitu).
                // Zde nepotřebujeme žádné speciální mapování, protože názvy vlastností se shodují.
                CreateMap<CreateCustomerDto, Customer>();

                // Komentář: Mapování z UpdateCustomerDto na Customer (entitu).
                CreateMap<UpdateCustomerDto, Customer>();

                // Komentář: Mapování z PatchCustomerDto na Customer (entitu).
                // Zde je mapování složitější, protože chceme aktualizovat jen ta pole, která jsou poslána.
                // AutoMapper má pro to specifické přístupy, ale pro PATCH je často lepší
                // ruční kontrola null hodnot, jak jsme to udělali v kontroleru,
                // nebo použít knihovny jako JsonPatch. Nicméně, pro základní mapování
                // to můžeme definovat takto, ale buď opatrný/á s null hodnotami.
                // Pro PATCH je často lepší ponechat ruční logiku v kontroleru,
                // pokud nechceš použít složitější řešení jako JsonPatch.
                // Pokud bys chtěl/a mapovat jen ne-null hodnoty, musel/a bys to specifikovat.
                // Pro jednoduchost a kontrolu nad PATCH operací zatím doporučuji ponechat
                // ruční kontrolu v kontroleru, jak jsme to udělali.
                // Nicméně, pro úplnost:
                // CreateMap<PatchCustomerDto, Customer>()
                //     .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                // Toto by mapovalo jen ne-null hodnoty ze zdroje.
            }
        }
    }
    