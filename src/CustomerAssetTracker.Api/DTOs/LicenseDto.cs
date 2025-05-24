using System.ComponentModel.DataAnnotations;

namespace CustomerAssetTracker.Api.DTOs
{
    public class LicenseDto
    {
        public int Id { get; set; }
        public string Software { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Vendor { get; set; } = string.Empty;
        public DateTime? MaintenanceContract { get; set; }
        public string LicenseType { get; set; } = string.Empty; 

        public int? MachineId { get; set; }
        public string? MachineName { get; set; } 

        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty; 
    }

    //Base DTO License class, which is used for both creating and updating licenses.
    public abstract class BaseeLicenseDto
    {
        [Required(ErrorMessage = "Software field is mandatory.")]
        public string Software { get; set; } = string.Empty;

        [Required(ErrorMessage = "Version field is mandatory.")]
        public string Version { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vendor field is mandatory.")]
        public string Vendor { get; set; } = string.Empty;

        [DataType(DataType.Date, ErrorMessage = "Maintenance Contract field must be a valid date.")]
        public DateTime? MaintenanceContract { get; set; }

        [Required(ErrorMessage = "License type is mandatory.")]
        public string LicenseType { get; set; } = string.Empty;

        public int? MachineId { get; set; } // Possible null if not associated with a machine
        [Required(ErrorMessage = "Customer ID is mandatory.")]
        public int CustomerId { get; set; }
    }

    public class CreateLicenseDto : BaseeLicenseDto
    {
        // This class inherits from BaseeLicenseDto and can be extended if needed.
    }

    public class UpdateLicenseDto : BaseeLicenseDto
    {
        // This class inherits from BaseeLicenseDto and can be extended if needed.
    }

    public class PatchLicenseDto
    {
        public string? Software { get; set; }
        public string? Version { get; set; }
        public string? Vendor { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Maintenance Contract field must be a valid date.")]
        public DateTime? MaintenanceContract { get; set; }
        public string? LicenseType { get; set; }
        public int? MachineId { get; set; }
        public int? CustomerId { get; set; }
    }

}