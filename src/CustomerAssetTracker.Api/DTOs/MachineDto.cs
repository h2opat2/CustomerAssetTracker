
using System.ComponentModel.DataAnnotations;

namespace CustomerAssetTracker.Api.DTOs
{

    public class MachineDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public string MachineType { get; set; } = string.Empty;

        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        public int LicenseCount { get; set; }
        public int ServiceRecordCount { get; set; }
    }

    //Base DTO Machine class, which is used for both creating and updating classes.
    public abstract class BaseMachineDto
    {
        [Required(ErrorMessage = "Customer ID field is mandatory.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Machine name field is mandatory.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Serial number field is mandatory.")]
        public string SerialNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Manufacturer field is mandatory.")]
        public string Manufacturer { get; set; } = string.Empty;

        [Required(ErrorMessage = "Purchase date field is mandatory.")]
        [DataType(DataType.Date, ErrorMessage = "Purchase date must be a valid date.")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "Machine type field is mandatory.")]
        public string MachineType { get; set; } = string.Empty; //

    }

    public class CreateMachineDto : BaseMachineDto
    {
        // This class inherits from BaseMachineDto and can be extended if needed.
    }

    public class UpdateMachineDto : BaseMachineDto
    {
        // This class inherits from BaseMachineDto and can be extended if needed.
    }

    public class PatchMachineDto
    {
        public string? Name { get; set; }
        public string? SerialNumber { get; set; }
        public string? Manufacturer { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Purchase date must be a valid date.")]
        public DateTime? PurchaseDate { get; set; }
        public string? MachineType { get; set; }
        public int? CustomerId { get; set; }
    }
}