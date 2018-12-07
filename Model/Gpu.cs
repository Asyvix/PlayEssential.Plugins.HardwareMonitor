using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayEssential.Plugins.HardwareMonitor.Model
{
    public class Gpu
    {
        public string Name { get; set; }
        public float Clock { get; set; }
        public float Load { get; set; }
        public float GpuMemoryFree { get; set; }
        public float GpuMemoryUsed { get; set; }
        public float GpuMemoryTotal { get; set; }
        public float Temperature { get; set; }
    }
}
