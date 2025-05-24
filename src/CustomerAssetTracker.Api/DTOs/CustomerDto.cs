using System.ComponentModel.DataAnnotations;

namespace CustomerAssetTracker.Api.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsForeign { get; set; }
        public int MachineCount { get; set; }
        public int LicenseCount { get; set; }
    }

    //Base DTO Customer class, which is used for both creating and updating classes.
    public abstract class BaseCustomerDto
    {
        [Required(ErrorMessage = "Name field is mandatory")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address field is mandatory")]
        public string Address { get; set; } = string.Empty;
        public bool IsForeign { get; set; }
    }
    public class CreateCustomerDto : BaseCustomerDto
    {
        // This class inherits from BaseCustomerDto and can be extended if needed.
    }
    public class UpdateCustomerDto : BaseCustomerDto
    {   
        // This class inherits from BaseCustomerDto and can be extended if needed.   
    }
        
    public class PatchCustomerDto
    {
        public string? Name { get; set; } 
        public string? Address { get; set; }
        public bool? IsForeign { get; set; } 
    }
}
    