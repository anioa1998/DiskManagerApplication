using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DiskManagerApplication.DriveOperations.S.M.A.R.T
{
    public class SmartBuilder : ISmartBuilder
    {
        private ManagementScope scope;
        private ManagementObjectSearcher searcher;
        private Dictionary<int, HardDriveData> allDrivesDictionary;

        public void SetScope(ManagementScope scope)
        {
            this.scope = scope;
        }
        public void SetDriveStorage()
        {
            allDrivesDictionary = new Dictionary<int, HardDriveData>();
        }
        public void Build()
        {
            throw new NotImplementedException();
        }


    }
}
