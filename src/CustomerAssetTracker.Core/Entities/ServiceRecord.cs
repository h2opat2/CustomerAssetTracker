namespace CustomerAssetTracker.Core;

public class ServiceRecord
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Technician { get; set; }
    public string Text { get; set; }
    public Machine Machine { get; set; }
    public int MachineId { get; set; }
}
