using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ext4Explorer
{
    public class PhysicalDiskInfo
    {
        public int DiskNumber { get; set; }
        public string DeviceID { get; set; }  // e.g., \\.\PHYSICALDRIVE0
        public string Model { get; set; }
        public ulong Size { get; set; }

        public override string ToString()
        {
            return $"Disk {DiskNumber} - {Model} - {Size / (1024 * 1024 * 1024)} GB";
        }
    }

}
