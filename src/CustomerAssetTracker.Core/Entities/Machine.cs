namespace CustomerAssetTracker.Core;

public class Machine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public string Manufacturer { get; set; }
    public DateTime PurchaseDate { get; set; }
    public Customer Customer { get; set; }
    public int CustomerId { get; set; } 
    public List<License> Licenses { get; set; }

    public MachineTypes MachineType { get; set; }

    public enum MachineTypes
    {
        Cmm, Arm, HandHeldScanner, LaserTracker, Microscop,
        Profilprojector, LengthGauge, DataUnit, Other
    }

    public List<ServiceRecord> ServiceRecords { get; set; }

    public Machine()
    {
        Licenses = new List<License>();
        ServiceRecords = new List<ServiceRecord>();
    }

    public string GetFullName()
    {
        return $"{MachineType}: {Name}, SN: {SerialNumber} ({Manufacturer})";
    }
    

}
