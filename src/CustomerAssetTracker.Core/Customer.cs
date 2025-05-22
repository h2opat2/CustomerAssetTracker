namespace CustomerAssetTracker.Core;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public bool IsForeign { get; set; }
    public List<Machine> Machines { get; set; }
    public List<License> Licenses { get; set; }
    
    public Customer()
    {
        Machines = new List<Machine>();
        Licenses = new List<License>();
    }


}
