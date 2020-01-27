using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace DiskManagerApplication.DriveOperations.S.M.A.R.T
{
    public interface ISmartBuilder
    {
        void SetScope(ManagementScope scope);
        void SetDriveStorage();
        void Build();
    }
}
