using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
namespace DiskManagerApplication
{
    public class DiskPartitionInformation
    {
        ManagementScope scope { get; set; }
        public DiskPartitionInformation(ManagementScope scope)
        {
            this.scope = scope;
        }
    }
}
