using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using DiskManagerApplication.DriveOperations.S.M.A.R.T;

namespace DiskManagerApplication
{
    public class DriveData
    {
        //ManagementScope scope { get; set; }
        //public SMARTData(ManagementScope scope)
        //{
        //    this.scope = scope;
        //}
        public int Index { get; set; }
        public bool IsOK { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Serial { get; set; }
        public Dictionary<int, SmartObject> Attributes = new Dictionary<int, SmartObject>() {
                {0x00, new SmartObject("Invalid")},
                {0x01, new SmartObject("Raw read error rate")},
                {0x02, new SmartObject("Throughput performance")},
                {0x03, new SmartObject("Spinup time")},
                {0x04, new SmartObject("Start/Stop count")},
                {0x05, new SmartObject("Reallocated sector count")},
                {0x06, new SmartObject("Read channel margin")},
                {0x07, new SmartObject("Seek error rate")},
                {0x08, new SmartObject("Seek timer performance")},
                {0x09, new SmartObject("Power-on hours count")},
                {0x0A, new SmartObject("Spinup retry count")},
                {0x0B, new SmartObject("Calibration retry count")},
                {0x0C, new SmartObject("Power cycle count")},
                {0x0D, new SmartObject("Soft read error rate")},
                {0xB8, new SmartObject("End-to-End error")},
                {0xBE, new SmartObject("Airflow Temperature")},
                {0xBF, new SmartObject("G-sense error rate")},
                {0xC0, new SmartObject("Power-off retract count")},
                {0xC1, new SmartObject("Load/Unload cycle count")},
                {0xC2, new SmartObject("HDD temperature")},
                {0xC3, new SmartObject("Hardware ECC recovered")},
                {0xC4, new SmartObject("Reallocation count")},
                {0xC5, new SmartObject("Current pending sector count")},
                {0xC6, new SmartObject("Offline scan uncorrectable count")},
                {0xC7, new SmartObject("UDMA CRC error rate")},
                {0xC8, new SmartObject("Write error rate")},
                {0xC9, new SmartObject("Soft read error rate")},
                {0xCA, new SmartObject("Data Address Mark errors")},
                {0xCB, new SmartObject("Run out cancel")},
                {0xCC, new SmartObject("Soft ECC correction")},
                {0xCD, new SmartObject("Thermal asperity rate (TAR)")},
                {0xCE, new SmartObject("Flying height")},
                {0xCF, new SmartObject("Spin high current")},
                {0xD0, new SmartObject("Spin buzz")},
                {0xD1, new SmartObject("Offline seek performance")},
                {0xDC, new SmartObject("Disk shift")},
                {0xDD, new SmartObject("G-sense error rate")},
                {0xDE, new SmartObject("Loaded hours")},
                {0xDF, new SmartObject("Load/unload retry count")},
                {0xE0, new SmartObject("Load friction")},
                {0xE1, new SmartObject("Load/Unload cycle count")},
                {0xE2, new SmartObject("Load-in time")},
                {0xE3, new SmartObject("Torque amplification count")},
                {0xE4, new SmartObject("Power-off retract count")},
                {0xE6, new SmartObject("GMR head amplitude")},
                {0xE7, new SmartObject("Temperature")},
                {0xF0, new SmartObject("Head flying hours")},
                {0xFA, new SmartObject("Read error retry rate")},

            };
    }
}
