using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayEssential.Plugins.HardwareMonitor.Model
{
    public class HardwareData
    {
        public Cpu Cpu { get; set; } = new Cpu();

        public Ram Ram { get; set; } = new Ram();

        public List<Storage> Storages { get; set; } = new List<Storage>();
        public List<Gpu> Gpus { get; set; }
    }
}
