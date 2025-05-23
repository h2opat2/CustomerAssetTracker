    using System.ComponentModel.DataAnnotations; // Pro validace

namespace CustomerAssetTracker.Api.DTOs
{
    // Komentář: DTO pro zobrazení detailů zákazníka.
    // Obsahuje pouze vlastnosti, které chceme vystavit klientovi.
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Inicializace pro null-safety
        public string Address { get; set; } = string.Empty;
        public bool IsForeign { get; set; }
        // Můžeme přidat i count Machines, Licenses, ale ne celé objekty kvůli cyklickým referencím
        public int MachineCount { get; set; }
        public int LicenseCount { get; set; }
    }

    // Komentář: DTO pro vytváření nového zákazníka.
    // Může mít odlišné validační požadavky nebo méně polí.
    public class CreateCustomerDto
    {
        [Required(ErrorMessage = "Jméno je povinné.")] // Příklad validace
        [MaxLength(100, ErrorMessage = "Jméno nesmí být delší než 100 znaků.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adresa je povinná.")]
        public string Address { get; set; } = string.Empty;

        public bool IsForeign { get; set; }
    }

    // Komentář: DTO pro aktualizaci existujícího zákazníka.
    // Může mít stejná pole jako CreateCustomerDto, ale může mít i Id.
    public class UpdateCustomerDto
    {
        [Required(ErrorMessage = "Jméno je povinné.")]
        [MaxLength(100, ErrorMessage = "Jméno nesmí být delší než 100 znaků.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adresa je povinná.")]
        public string Address { get; set; } = string.Empty;

        public bool IsForeign { get; set; }
    }
        
    public class PatchCustomerDto
    {
        public string? Name { get; set; } 
        public string? Address { get; set; }
        public bool? IsForeign { get; set; } 
    }
    }
    