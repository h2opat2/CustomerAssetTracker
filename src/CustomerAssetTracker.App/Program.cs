using CustomerAssetTracker.Core;

Cmm cmm = new Cmm();
cmm.Name = "LK Altera S";
cmm.Manufacturer = "LK Metrology";
cmm.SerialNumber = "123/002017A";
cmm.Type = Cmm.CmmType.CNC;
cmm.SizeX = 15;
cmm.SizeY = 10;
cmm.SizeZ = 8;

Console.WriteLine(cmm.GetFullName());
