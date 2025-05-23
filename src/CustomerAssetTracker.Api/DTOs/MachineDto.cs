
using System;
using System.ComponentModel.DataAnnotations;
using CustomerAssetTracker.Core; // Pro přístup k enumům jako MachineTypes, LicenseType

namespace CustomerAssetTracker.Api.DTOs
{
    // Komentář: DTOs pro entitu Machine

    // DTO pro zobrazení detailů stroje.
    // Zahrnuje CustomerId a CustomerName pro snadnější zobrazení.
    // Dále počty souvisejících záznamů.
    public class MachineDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public string MachineType { get; set; } = string.Empty; // Reprezentace enum jako string

        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty; // Pro zobrazení jména zákazníka

        public int LicenseCount { get; set; }
        public int ServiceRecordCount { get; set; }
    }

    // DTO pro vytváření nového stroje.
    public class CreateMachineDto
    {
        [Required(ErrorMessage = "ID zákazníka je povinné.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Název stroje je povinný.")]
        [MaxLength(100, ErrorMessage = "Název stroje nesmí být delší než 100 znaků.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sériové číslo je povinné.")]
        [MaxLength(50, ErrorMessage = "Sériové číslo nesmí být delší než 50 znaků.")]
        public string SerialNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Výrobce je povinný.")]
        [MaxLength(100, ErrorMessage = "Výrobce nesmí být delší než 100 znaků.")]
        public string Manufacturer { get; set; } = string.Empty;

        [Required(ErrorMessage = "Datum nákupu je povinné.")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "Typ stroje je povinný.")]
        public string MachineType { get; set; } = string.Empty; // Očekává string, který se namapuje na enum

    }

    // DTO pro aktualizaci existujícího stroje (úplná náhrada).
    public class UpdateMachineDto
    {
        [Required(ErrorMessage = "ID zákazníka je povinné.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Název stroje je povinný.")]
        [MaxLength(100, ErrorMessage = "Název stroje nesmí být delší než 100 znaků.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sériové číslo je povinné.")]
        [MaxLength(50, ErrorMessage = "Sériové číslo nesmí být delší než 50 znaků.")]
        public string SerialNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Výrobce je povinný.")]
        [MaxLength(100, ErrorMessage = "Výrobce nesmí být delší než 100 znaků.")]
        public string Manufacturer { get; set; } = string.Empty;

        [Required(ErrorMessage = "Datum nákupu je povinné.")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "Typ stroje je povinný.")]
        public string MachineType { get; set; } = string.Empty;

    }

    // DTO pro částečnou aktualizaci stroje (PATCH).
    public class PatchMachineDto
    {
        public string? Name { get; set; }
        public string? SerialNumber { get; set; }
        public string? Manufacturer { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string? MachineType { get; set; }
        public int? CustomerId { get; set; }
    }
}