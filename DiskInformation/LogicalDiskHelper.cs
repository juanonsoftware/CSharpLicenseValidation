using System.Collections.Generic;
using System.Management;

namespace DiskInformation
{
    public static class LogicalDiskHelper
    {
        public static IList<LogicalDisk> GetLogicalDisks()
        {
            var list = new List<LogicalDisk>();
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk");

            foreach (ManagementObject mo in searcher.Get())
            {
                list.Add(ParseDiskDrive(mo));
            }

            return list;
        }

        private static LogicalDisk ParseDiskDrive(ManagementObject mo)
        {
            return new LogicalDisk()
            {
                Name = $"{mo.GetPropertyValue("Name")}",
                FileSystem = $"{mo.GetPropertyValue("FileSystem")}",
                VolumeSerialNumber = $"{mo.GetPropertyValue("VolumeSerialNumber")}",
            };
        }
    }
}
