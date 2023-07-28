using LicenseValidationHelper;

namespace LicenseTestingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("This sample program will validate a license file with hardware information used to generate the license.");

            var licenseFile = @"D:\Dev\Github\CsharpLicenseValidating\LicenseTestingApp\LicenseKey.txt";
            var password = "Password$To!BuildLicense@2023";

            // Now validating the license
            try
            {
                LicenseHelper.ValidateLicense(licenseFile, password);
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine("UNABLE TO VALIDATE THE LICENSE");
                Console.WriteLine("Error is: " + ex.Message);
                return;
            }

            Console.WriteLine("License is GOOD, continue...");
            Console.ReadLine();
        }
    }
}