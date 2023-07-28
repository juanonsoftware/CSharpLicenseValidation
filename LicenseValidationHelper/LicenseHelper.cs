using DiskInformation;
using SOStringCipher;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LicenseValidationHelper
{
    public static class LicenseHelper
    {
        public static void ValidateLicense(string licenseFile, string encryptionPassword)
        {
            var sb = new StringBuilder();

            try
            {
                // 1. Computer FQDN
                sb.AppendFormat("{0}/", GetComputerFQDN());
                // 2. First IP address
                sb.AppendFormat("{0}/", GetFirstIpAddress());
                // 3. First logical disk
                sb.AppendFormat("{0}/", GetFirstLogicalDiskInfo());
                // 4. First disk drive
                sb.AppendFormat("{0}", GetFirstDiskDriveInfo());

                var licenseKey = File.ReadAllText(licenseFile);
                var plainText = StringCipher.Decrypt(licenseKey, encryptionPassword);

                if (!sb.ToString().Equals(plainText))
                {
                    ThrowsLicenseError(new ArgumentException($"System info: '{sb}'"));
                }
            }
            catch (Exception ex)
            {
                ThrowsLicenseError(ex);
            }
        }

        private static void ThrowsLicenseError(Exception ex)
        {
            throw new ApplicationException("Invalid license key, please contact the provider for info at chienthang1819@gmail.com", ex);
        }

        private static string GetComputerFQDN()
        {
            return Dns.GetHostByName(Dns.GetHostName()).HostName;
        }

        private static string GetFirstIpAddress()
        {
            return Dns.GetHostByName(Dns.GetHostName()).AddressList
                .Where(x => x.AddressFamily == AddressFamily.InterNetwork)
                .First()
                .ToString();
        }

        private static string GetFirstLogicalDiskInfo()
        {
            var firstLD = LogicalDiskHelper.GetLogicalDisks().First();
            return $"{firstLD.Name}/{firstLD.VolumeSerialNumber}";
        }

        private static string GetFirstDiskDriveInfo()
        {
            var firstDD = DiskDriveHelper.GetDiskDrives().First();
            return $"{firstDD.Model}/{firstDD.SerialNumber}/{firstDD.PNPDeviceID}";
        }
    }
}
