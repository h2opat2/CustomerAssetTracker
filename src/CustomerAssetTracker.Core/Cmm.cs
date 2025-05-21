using System.Runtime.InteropServices;

namespace CustomerAssetTracker.Core;

public class Cmm : Machine
{
    public int SizeX { get; set; }
    public int SizeY { get; set; }
    public int SizeZ { get; set; }
    public CmmType Type { get; set; }

    public enum CmmType
    {
        Manual,
        CNC
    }
}
