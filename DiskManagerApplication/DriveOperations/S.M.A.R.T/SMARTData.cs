using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace DiskManagerApplication
{
    public class SMARTData
    {
        ManagementScope scope { get; set; }
        public SMARTData(ManagementScope scope)
        {
            this.scope = scope;
        }
    }
}
