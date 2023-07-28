$outputFile = Join-Path $PSScriptRoot ("SystemInfo" + (Get-Date).ToString("yyyyMMdd-hhmmss") + ".txt")

# Get computer FQDN hostname
"1. Computer FQDN" >> $outputFile
[System.Net.Dns]::GetHostByName("LocalHost").HostName >> $outputFile

# Get first IP address
"2. First IP address" >> $outputFile
[System.Net.Dns]::GetHostByName($env:computerName).AddressList[0].IPAddressToString >> $outputFile

# Get first logical disk info
"3. First logical disk" >> $outputFile
Get-WmiObject Win32_LogicalDisk | Select-Object -First 1 -Property Name, VolumeSerialNumber >> $outputFile

# Get first disk drive info
"4. First disk drive" >> $outputFile
Get-WmiObject Win32_DiskDrive | Select-Object -First 1 -Property Model, InterfaceType, SerialNumber, PNPDeviceID >> $outputFile

"All output saved in file $outputFile"