using System;
using System.ComponentModel.DataAnnotations;
using CustomerAssetTracker.Core; // Pro přístup k enumům jako MachineTypes, LicenseType

namespace CustomerAssetTracker.Api.DTOs
{
    // Komentář: DTOs pro entitu ServiceRecord

    // DTO pro zobrazení detailů servisního záznamu.
    public class ServiceRecordDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Technician { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int MachineId { get; set; }
        public string MachineName { get; set; } = string.Empty; // Pro zobrazení názvu stroje
    }

    // DTO pro vytváření nového servisního záznamu.
    public class CreateServiceRecordDto
    {
        [Required(ErrorMessage = "Datum je povinné.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Technik je povinný.")]
        public string Technician { get; set; } = string.Empty;

        [Required(ErrorMessage = "Text záznamu je povinný.")]
        public string Text { get; set; } = string.Empty;

        [Required(ErrorMessage = "ID stroje je povinné.")]
        public int MachineId { get; set; }
    }

    // DTO pro aktualizaci existujícího servisního záznamu.
    public class UpdateServiceRecordDto
    {
        [Required(ErrorMessage = "Datum je povinné.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Technik je povinný.")]
        public string Technician { get; set; } = string.Empty;

        [Required(ErrorMessage = "Text záznamu je povinný.")]
        public string Text { get; set; } = string.Empty;

        [Required(ErrorMessage = "ID stroje je povinné.")]
        public int MachineId { get; set; }
    }

    // DTO pro částečnou aktualizaci servisního záznamu.
    public class PatchServiceRecordDto
    {
        public DateTime? Date { get; set; }
        public string? Technician { get; set; }
        public string? Text { get; set; }
        public int? MachineId { get; set; }
    }
}