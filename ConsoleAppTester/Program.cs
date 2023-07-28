using DiskInformation;

namespace ConsoleAppTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Get disks information");
            Console.WriteLine("---------");

            var disks = DiskDriveHelper.GetDiskDrives();

            foreach (var disk in disks)
            {
                Console.WriteLine($"Model: {disk.Model}");
                Console.WriteLine($"Type: {disk.InterfaceType}");
                Console.WriteLine($"SerialNo: {disk.SerialNumber}");
                Console.WriteLine($"Size: {disk.Size}");
                Console.WriteLine($"PNPDeviceID: {disk.PNPDeviceID}");
            }

            Console.WriteLine("---------");
            Console.WriteLine("ToString & parse FromString");

            var s = DiskDriveHelper.Serialize(disks.First());
            var d = DiskDriveHelper.GetFromString(s);

            Console.WriteLine($"Model: {d.Model}");
            Console.WriteLine($"Type: {d.InterfaceType}");
            Console.WriteLine($"SerialNo: {d.SerialNumber}");
            Console.WriteLine($"PNPDeviceID: {d.PNPDeviceID}");


            Console.WriteLine("---------");
            Console.WriteLine("Logical disks");
            foreach(var disk in LogicalDiskHelper.GetLogicalDisks())
            {
                Console.WriteLine($"Name: {disk.Name}");
                Console.WriteLine($"VolumeSerialNumber: {disk.VolumeSerialNumber}");
                Console.WriteLine($"FileSystem: {disk.FileSystem}");
            }
        }
    }
}