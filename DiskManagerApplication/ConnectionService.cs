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
    }
}
