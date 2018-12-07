using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using asyvix.PlayEssential.Services.PlugIn.v.r1;
using Newtonsoft.Json;
using Timer = System.Timers.Timer;

namespace PlayEssential.Plugins.HardwareMonitor
{
    public class Startup : IPlayEssentialPlugin
    {
        private Monitor _monitor;
        private IPlayEssentialPluginContext _context;
        public void StartUp(IPlayEssentialPluginContext context)
        {
            _context = context;
            _monitor = new Monitor();
            _monitor.Computer.CPUEnabled = true;
            _monitor.Computer.RAMEnabled = true;
            _monitor.Computer.HDDEnabled = true;
            _monitor.Computer.GPUEnabled = true;
            _monitor.Computer.Open();
            _tmread = new Thread(new ThreadStart(MonitoringStart));
            _tmread.Start();
        }

        private Thread _tmread;


        public void MonitoringStart()
        {
            while (true)
            {
                _context.Bridge.Publish(JsonConvert.SerializeObject(_monitor.Tick()));
                Thread.Sleep(3000);
            }
            
        }


        public IPlayEssentialServiceBridge Bridge { get; set; }
        public string ServiceName { get; } = "HardwareMonitor";
        public string ServiceDescription { get; } = "MonitoringHardware";
        public string CreatedBy { get; } = "Asyvix";
        public string CompanyName { get; } = "PlayEssential";
        public string EulaLink { get; } = "null";
        public string Copyright { get; } = "c@ asyvix 2015-2019";
    }
}
