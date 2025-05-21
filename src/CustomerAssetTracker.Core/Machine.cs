namespace CustomerAssetTracker.Core;

public class Machine
{
    public string Name { get; set;}
    public string SerialNumber { get; set;}
    public string Manufacturer { get; set;}

    public string GetFullName()
    {
        return $"{Name}, SN: {SerialNumber} ({Manufacturer})";
    }
}
