using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Collections;

namespace DiskManagerApplication
{
    public class DiskPartitionInformation
    {
        ManagementScope scope { get; set; }
        ManagementObjectCollection queryCollection { get; set; }

        ConnectionService connectionService; 

        double sizeGB { get; set; }
        int returnCode { get; set; }

        public DiskPartitionInformation(ManagementScope scope)
        {
            this.scope = scope;
            connectionService = new ConnectionService(scope);
        }

        public bool ShowDiskInformation()
        {
            try
            {
                queryCollection = connectionService.GetQueryCollectionFromWin32Class("Win32_DiskDrive");

                foreach (ManagementObject diskData in queryCollection)
                {

                    Console.WriteLine("---------------------------------------------");

                    Console.WriteLine("Nazwa dysku: {0}", diskData["Name"]);
                    Console.WriteLine("Model: {0}", diskData["Model"]);
                    Console.WriteLine("Rozmiar: {0} GB", ConvertToGB(Convert.ToInt64(diskData["Size"])));
                    Console.WriteLine("Numer seryjny {0}", diskData["SerialNumber"]);
                    Console.WriteLine("Całkowita ilość sektorów: {0}", diskData["TotalSectors"]);
                    Console.WriteLine("Status: {0}", diskData["Status"]);
                    Console.WriteLine("Typ interfejsu: {0}", diskData["InterfaceType"]);
                    Console.WriteLine("Rodzaj pamięci: {0}", diskData["MediaType"]);
                    Console.WriteLine("Rewizja Firmware: {0}", diskData["FirmwareRevision"]);
                    Console.WriteLine("Opis zdolności:");
                    IEnumerable enumerable = diskData["CapabilityDescriptions"] as IEnumerable;
                    if (enumerable != null)
                    {
                        foreach (object element in enumerable)
                        {
                            Console.WriteLine("- " + element);
                        }
                    }

                    Console.WriteLine("---------------------------------------------\n");
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("DiskPartitionInformation Error: " + e.Message);
            }

            do
            {
                returnCode = ReturnMenu();
            } while (returnCode != 0 && returnCode != 1);

            if (returnCode == 0)
                return true;

            Console.Clear();
            return false;
        }

        public bool ShowPartitionInformation()
        {
            try
            {
                queryCollection = connectionService.GetQueryCollectionFromWin32Class("Win32_DiskPartition");

                foreach (ManagementObject partData in queryCollection)
                {
                    
                    Console.WriteLine("---------------------------------------------");

                    Console.WriteLine("Nazwa partycji: {0}", partData["Name"]);
                    Console.WriteLine("Indeks dysku: {0}", partData["DiskIndex"]);
                    Console.WriteLine("Rozmiar: {0} GB", ConvertToGB(Convert.ToInt64(partData["Size"])));
                    Console.WriteLine("Ilość bloków: {0}", partData["NumberOfBlocks"]);
                    Console.WriteLine("Wielkość bloków: {0}", partData["BlockSize"]);
                    Console.WriteLine("Typ: {0}", partData["Type"]);
                    Console.WriteLine("Czy to partycja rozruchowa? : {0}", partData["Bootable"]);

                    Console.WriteLine("---------------------------------------------\n");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("DiskPartitionInformation Error: " + e.Message);
            }

            do
            {
                returnCode = ReturnMenu();
            } while (returnCode != 0 && returnCode != 1);

            if (returnCode == 0)
                return true;

            Console.Clear();
            return false;
        }

        public bool ShowLogicalDiskInformation()
        {
            try
            {
                queryCollection = connectionService.GetQueryCollectionFromWin32Class("Win32_LogicalDisk");

                foreach (ManagementObject logicData in queryCollection)
                {
                    Console.WriteLine("---------------------------------------------");

                    Console.WriteLine("Nazwa dysku logicznego: {0}", logicData["Name"]);
                    Console.WriteLine("Opis: {0}", logicData["Description"]);
                    Console.WriteLine("Rozmiar: {0} GB", ConvertToGB(Convert.ToInt64(logicData["Size"])));
                    Console.WriteLine("System plików: {0}", logicData["FileSystem"]);
                    Console.WriteLine("Wolne miejsce: {0} GB", ConvertToGB(Convert.ToInt64(logicData["FreeSpace"])));
                    Console.WriteLine("Nazwa wolumenu: {0}", logicData["VolumeName"]);
                    Console.WriteLine("Numer seryjny: {0}", logicData["VolumeSerialNumber"]);

                    Console.WriteLine("---------------------------------------------\n");
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("DiskPartitionInformation Error: " + e.Message);
            }

            do
            {
                returnCode = ReturnMenu();
            } while (returnCode != 0 && returnCode != 1);

            if (returnCode == 0)
                return true;

            Console.Clear();
            return false;
        }

        private int ReturnMenu()
        {
            Console.WriteLine("[0] - Wyjście z programu");
            Console.WriteLine("[1] - Wróć do menu głównego");
            Console.Write("Wybieram: ");
            return Convert.ToInt32(Console.ReadLine()); 
        }

        private double ConvertToGB(long bytes)
        {
            return bytes / 1000000000;
        }
    }
}
