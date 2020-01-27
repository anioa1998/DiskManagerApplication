using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace DiskManagerApplication
{
    public class ConnectionService
    {
        ManagementScope scope { get; set; }
        ObjectQuery query { get; set; }
        ManagementObjectSearcher searcher { get; set; }

        public ConnectionService() { }

        public ConnectionService(ManagementScope scope)
        {
            this.scope = scope;
        }

        public ImpersonationLevel SetImpersonationLevel()
        {
            return ImpersonationLevel.Impersonate;
        }

        public ManagementScope GetCIMConnection(ConnectionOptions options)
        {
            string computerName = Environment.MachineName;
            if (computerName == null || computerName == "/0")
                throw new Exception("CIM Connection failed");
            Console.WriteLine("Your computer name: {0}\n", computerName);
            return new ManagementScope($"\\\\{computerName}\\root\\cimv2", options);
        }

        public ManagementObjectCollection GetQueryCollectionFromWin32Class(string ClassName)
        {
            query = new ObjectQuery($"SELECT * FROM {ClassName}");
            if (query == null)
                throw new Exception("Nie wykonano zapytania - ObjectQuery Failed");

            searcher = new ManagementObjectSearcher(scope, query);
            if (searcher == null)
                throw new Exception("Nie znaleziono obiektów spełniających zapytanie - ManagementObjectSearcher Failed");
            return searcher.Get();
        }

        public ManagementObjectCollection GetQueryCollectionFromWMI(string ClassName)
        {
            searcher = new ManagementObjectSearcher("Select * from Win32_DiskDrive");
            if (searcher == null)
                throw new Exception("Nie znaleziono obiektów spełniających zapytanie - ManagementObjectSearcher Failed");
            searcher.Scope = new ManagementScope(@"\root\wmi");
            query = new ObjectQuery($"SELECT * FROM {ClassName}");
            if (query == null)
                throw new Exception("Nie wykonano zapytania - ObjectQuery Failed");

            return searcher.Get();
        }
    }
}
