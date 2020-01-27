using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskManagerApplication.DriveOperations.S.M.A.R.T
{
    public class SmartObjectHelper
    {
        public int id { get; set; }
        public int flags { get; set; }
        public bool failureIsComing { get; set; }
        public int value { get; set; }
        public int worst { get; set; }
        public int vendorData { get; set; }
        public int threshold { get; set; }
    }
}
