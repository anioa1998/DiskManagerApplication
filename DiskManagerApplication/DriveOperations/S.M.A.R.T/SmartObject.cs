using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskManagerApplication.DriveOperations.S.M.A.R.T
{
    public class SmartObject
    {
        public string Attribute { get; set; }
        public int Current { get; set; }
        public int Worst { get; set; }
        public int Threshold { get; set; }
        public int Data { get; set; }
        public bool Status { get; set; }

        public SmartObject(string attributeName)
        {
            this.Attribute = attributeName;
        }

        public bool CheckIfHasData
        {
            get
            {
                if (Current == 0 && Worst == 0 && Threshold == 0 && Data == 0)
                    return false;
                return true;
            }
        }


    }
}
