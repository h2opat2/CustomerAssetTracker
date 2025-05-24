using System.ComponentModel.DataAnnotations;

namespace CustomerAssetTracker.Api.DTOs
{
    public class ServiceRecordDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Technician { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int MachineId { get; set; }
        public string MachineName { get; set; } = string.Empty;
    }

    //Base DTO Service Record class, which is used for both creating and updating classes.
    public abstract class BaseServiceRecordDto
    {
        [Required(ErrorMessage = "Date field is mandatory.")]
        [DataType(DataType.Date, ErrorMessage = "Date must be a valid date.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Technician field is mandatory.")]
        public string Technician { get; set; } = string.Empty;

        [Required(ErrorMessage = "Text field is mandatory.")]
        [MaxLength(500, ErrorMessage = "Text must not exceed 500 characters.")]
        public string Text { get; set; } = string.Empty;

        [Required(ErrorMessage = "Machine ID field is mandatory.")]
        public int MachineId { get; set; }
    }

    public class CreateServiceRecordDto : BaseServiceRecordDto
    {
        // This class inherits from BaseServiceRecordDto and can be extended if needed.
    }

    // DTO pro aktualizaci existujícího servisního záznamu.
    public class UpdateServiceRecordDto : BaseServiceRecordDto
    {
        // This class inherits from BaseServiceRecordDto and can be extended if needed.
    }

    public class PatchServiceRecordDto
    {
        [DataType(DataType.Date, ErrorMessage = "Date must be a valid date.")]
        public DateTime? Date { get; set; }
        public string? Technician { get; set; }

        [MaxLength(500, ErrorMessage = "Text must not exceed 500 characters.")]
        public string? Text { get; set; }
        public int? MachineId { get; set; }
    }
}