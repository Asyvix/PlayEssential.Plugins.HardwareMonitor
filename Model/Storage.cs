using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayEssential.Plugins.HardwareMonitor.Model
{
    public class Storage
    {
        public string Name { get; set; }
        public float UsagePercent { get; set; }
        public float Usage { get; set; }
        public float Temperature { get; set; }
    }
}
