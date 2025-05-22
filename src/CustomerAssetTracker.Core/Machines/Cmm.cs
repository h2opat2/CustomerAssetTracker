using System.Runtime.InteropServices;

namespace CustomerAssetTracker.Core;

public class Cmm : Machine
{

    private int _sizeX;
    public int SizeX
    {
        get { return _sizeX; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Size X must be a positive number");
            }
            _sizeX = value;
        }
    }

    private int _sizeY;
    public int SizeY
    {
        get { return _sizeY; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Size Y must be a positive number");
            }
            _sizeY = value;
        }
    }
    private int _sizeZ;
    public int SizeZ
    {
        get { return _sizeZ; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Size Z must be a positive number");
            }
            _sizeZ = value;
        }
    }


    public CmmType Type { get; set; }

    public enum CmmType
    {
        Manual,
        CNC
    }
    
    public Cmm()
    {
        MachineType = MachineTypes.Cmm;
    }
}
