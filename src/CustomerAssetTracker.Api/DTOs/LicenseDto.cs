using System;
using System.ComponentModel.DataAnnotations;
using CustomerAssetTracker.Core; // Pro přístup k enumům jako MachineTypes, LicenseType

namespace CustomerAssetTracker.Api.DTOs
{
    // Komentář: DTOs pro entitu License

    // DTO pro zobrazení detailů licence.
    public class LicenseDto
    {
        public int Id { get; set; }
        public string Software { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Vendor { get; set; } = string.Empty;
        public DateTime? MaintenanceContract { get; set; }
        public string LicenseType { get; set; } = string.Empty; // Reprezentace enum jako string

        public int? MachineId { get; set; }
        public string? MachineName { get; set; } // Pro zobrazení názvu stroje (pokud je vázáno)

        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty; // Pro zobrazení jména zákazníka
    }

    // DTO pro vytváření nové licence.
    public class CreateLicenseDto
    {
        [Required(ErrorMessage = "Software je povinný.")]
        public string Software { get; set; } = string.Empty;

        [Required(ErrorMessage = "Verze je povinná.")]
        public string Version { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vendor je povinný.")]
        public string Vendor { get; set; } = string.Empty;

        public DateTime? MaintenanceContract { get; set; }

        [Required(ErrorMessage = "Typ licence je povinný.")]
        public string LicenseType { get; set; } = string.Empty;

        public int? MachineId { get; set; } // Může být null, pokud licence není vázána na stroj
        [Required(ErrorMessage = "ID zákazníka je povinné.")]
        public int CustomerId { get; set; }
    }

    // DTO pro aktualizaci existující licence.
    public class UpdateLicenseDto
    {
        [Required(ErrorMessage = "Software je povinný.")]
        public string Software { get; set; } = string.Empty;

        [Required(ErrorMessage = "Verze je povinná.")]
        public string Version { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vendor je povinný.")]
        public string Vendor { get; set; } = string.Empty;

        public DateTime? MaintenanceContract { get; set; }

        [Required(ErrorMessage = "Typ licence je povinný.")]
        public string LicenseType { get; set; } = string.Empty;

        public int? MachineId { get; set; }
        [Required(ErrorMessage = "ID zákazníka je povinné.")]
        public int CustomerId { get; set; }
    }

    // DTO pro částečnou aktualizaci licence.
    public class PatchLicenseDto
    {
        public string? Software { get; set; }
        public string? Version { get; set; }
        public string? Vendor { get; set; }
        public DateTime? MaintenanceContract { get; set; }
        public string? LicenseType { get; set; }
        public int? MachineId { get; set; }
        public int? CustomerId { get; set; }
    }

}