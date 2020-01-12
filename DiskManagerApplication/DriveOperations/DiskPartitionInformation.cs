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
        ObjectQuery query { get; set; }
        ManagementObjectSearcher searcher { get; set; }
        ManagementObjectCollection queryCollection { get; set; }

        public DiskPartitionInformation(ManagementScope scope)
        {
            this.scope = scope;
        }

        public void ShowDiskInformation()
        {
            double diskSizeGB;

            try
            {
                query = new ObjectQuery("SELECT * FROM Win32_DiskDrive");
                if (query == null)
                    throw new Exception("Nie wykonano zapytania - ObjectQuery Failed");

                searcher = new ManagementObjectSearcher(scope, query);
                if (searcher == null)
                    throw new Exception("Nie znaleziono obiektów spełniających zapytanie - ManagementObjectSearcher Failed");

                queryCollection = searcher.Get();

                foreach(ManagementObject diskData in queryCollection)
                {
                    diskSizeGB = Convert.ToInt64(diskData["Size"]) / 1000000;

                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("Nazwa dysku: {0}", diskData["Name"]);
                    Console.WriteLine("Model: {0}", diskData["Model"]);
                    Console.WriteLine("Rozmiar: {0} GB", diskSizeGB);
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
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("DiskPartitionInformation Error: " + e.Message);
            }
        }

        public void ShowPartitionInformation()
        {

        }
    }
}
