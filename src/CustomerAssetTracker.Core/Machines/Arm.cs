using System.Runtime.InteropServices;

namespace CustomerAssetTracker.Core;

public class Arm : Machine
{

    private int _size;
    public int Size
    {
        get { return _size; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Size must be a positive number");
            }
            _size = value;
        }
    }

    public ArmType Type { get; set; }

    public enum ArmType
    {
        SixAxes,
        SevenAxes
    }
    
    public Arm()
    {
        MachineType = MachineTypes.Arm;
    }
}
