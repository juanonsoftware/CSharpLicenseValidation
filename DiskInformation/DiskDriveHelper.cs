using System.Collections.Generic;
using System.Management;

namespace DiskInformation
{
    public static class DiskDriveHelper
    {
        public static IList<DiskDrive> GetDiskDrives()
        {
            var list = new List<DiskDrive>();
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject mo in searcher.Get())
            {
                list.Add(ParseDiskDrive(mo));
            }

            return list;
        }

        private static DiskDrive ParseDiskDrive(ManagementObject mo)
        {
            return new DiskDrive()
            {
                Model = $"{mo.GetPropertyValue("Model")}",
                InterfaceType = $"{mo.GetPropertyValue("InterfaceType")}",
                Description = $"{mo.GetPropertyValue("Description")}",
                SerialNumber = $"{mo.GetPropertyValue("SerialNumber")}",
                PNPDeviceID = $"{mo.GetPropertyValue("PNPDeviceID")}",
                Size = long.Parse((mo.GetPropertyValue("Size") == null) ? "0" : mo.GetPropertyValue("Size").ToString()),
            };
        }

        public static string Serialize(DiskDrive d)
        {
            return $"{d.InterfaceType}/{d.Model}/{d.SerialNumber}/{d.PNPDeviceID}";
        }

        public static DiskDrive GetFromString(string serialized)
        {
            var info = serialized.Split('/');

            return new DiskDrive
            {
                InterfaceType = info[0],
                Model = info[1],
                SerialNumber = info[2],
                PNPDeviceID = info[3]
            };
        }
    }
}