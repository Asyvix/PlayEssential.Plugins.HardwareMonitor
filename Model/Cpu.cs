using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayEssential.Plugins.HardwareMonitor.Model
{
    public class Cpu
    {
        public string Name { get; set; }
        public Dictionary<int, CpuCore> Cores { get; set; } = new Dictionary<int, CpuCore>();
        public int BusSpeed { get; set; }
        public float PackageTemperature { get; set; }
        public float TotalLoad { get; set; }

        //public float CpuPackagePower { get; set; }
        //public float CpuCorePower { get; set; }
        //public float CpuGraphicsPower { get; set; }
        //public float CpuDramPower { get; set; }

    }
}
