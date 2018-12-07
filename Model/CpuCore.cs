using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayEssential.Plugins.HardwareMonitor.Model
{
    public class CpuCore
    {
        public float Load { get; set; }
        public float Temperature { get; set; }
        public float Clock { get; set; }
    }
}
