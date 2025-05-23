namespace CustomerAssetTracker.Core;

public class License
{
    public int Id { get; set; }
    public string Software { get; set; }
    public string Version { get; set; }
    public string Vendor { get; set; }
    public DateTime? MaintenanceContract { get; set; }
    public Machine? Machine { get; set; }
    public int? MachineId { get; set; }
    public Customer Customer { get; set; }
    public int CustomerId { get; set; }
    public LicenseType Type { get; set; }
    public enum LicenseType
    {
        Software,
        Dongle,
        Floating
    }

}
