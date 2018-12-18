using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ExNet.Extensions;
using OpenHardwareMonitor.Hardware;
using PlayEssential.Plugins.HardwareMonitor.Model;

namespace PlayEssential.Plugins.HardwareMonitor
{
    public class Monitor
    {
        public Computer Computer = new Computer();

        public HardwareData Tick()
        {
            HardwareData data = new HardwareData();

            foreach (var hardwareItem in Computer.Hardware)
            {
                switch (hardwareItem.HardwareType)
                {
                    case HardwareType.CPU:
                        {                            
                        hardwareItem.Update();
                        data.Cpu.Name = hardwareItem.Name;
                        foreach (IHardware subHardware in hardwareItem.SubHardware)
                            subHardware.Update();
                        foreach (var sensor in hardwareItem.Sensors)
                        {
                            if (sensor.SensorType == SensorType.Temperature)
                            {
                                float value;
                                if (sensor.Value != null)
                                {
                                    value = sensor.Value.Value;
                                }
                                else
                                {
                                    value = 0;
                                }

                                if (sensor.Name.Contains("Package"))
                                {
                                    data.Cpu.PackageTemperature = value;
                                }
                                else
                                {
                                    int coreCount = sensor.Name.Split('#')[1].ToInt();
                                    if (data.Cpu.Cores.ContainsKey(coreCount))
                                    {

                                        data.Cpu.Cores[coreCount].Temperature = value;
                                    }
                                    else
                                    {
                                        data.Cpu.Cores.Add(coreCount, new CpuCore());
                                        data.Cpu.Cores[coreCount].Temperature = value;
                                    }
                                }
                            }

                            if (sensor.SensorType == SensorType.Load)
                            {

                                float value;
                                if (sensor.Value != null)
                                {
                                    value = sensor.Value.Value;
                                }
                                else
                                {
                                    value = 0;
                                }

                                if (sensor.Name.Contains("Total"))
                                {
                                    data.Cpu.TotalLoad = value;
                                }
                                else
                                {
                                    int coreCount = sensor.Name.Split('#')[1].ToInt();
                                    if (data.Cpu.Cores.ContainsKey(coreCount))
                                    {
                                        data.Cpu.Cores[coreCount].Load = value;
                                    }
                                    else
                                    {
                                        data.Cpu.Cores.Add(coreCount, new CpuCore());
                                        data.Cpu.Cores[coreCount].Load = value;
                                    }
                                }
                            }
                            if (sensor.SensorType == SensorType.Clock)
                            {

                                float value;
                                if (sensor.Value != null)
                                {
                                    value = sensor.Value.Value;
                                }
                                else
                                {
                                    value = 0;
                                }

                                if (sensor.Name.Contains("Speed"))
                                {
                                    data.Cpu.BusSpeed = (int)value;
                                }
                                else
                                {
                                    int coreCount = sensor.Name.Split('#')[1].ToInt();
                                    if (data.Cpu.Cores.ContainsKey(coreCount))
                                    {
                                        data.Cpu.Cores[coreCount].Clock = value;
                                    }
                                    else
                                    {
                                        data.Cpu.Cores.Add(coreCount, new CpuCore());
                                        data.Cpu.Cores[coreCount].Clock = value;
                                    }
                                }
                            }
                            }

                        break;
                        }

                    case HardwareType.RAM:
                        {
                            hardwareItem.Update();
                            foreach (IHardware subHardware in hardwareItem.SubHardware)
                                subHardware.Update();

                            foreach (var sensor in hardwareItem.Sensors)
                            {
                                if (sensor.SensorType == SensorType.Data)
                                {
                                    data.Ram.Usage = sensor.Value.HasValue ? (float)Math.Round(sensor.Value.Value, 1) : 0;
                                }
                                else if (sensor.SensorType == SensorType.Load)
                                {
                                    if (sensor.Value != null)
                                    {
                                        data.Ram.UsagePercent = sensor.Value.Value;
                                    }
                                    else
                                    {
                                        data.Ram.UsagePercent = sensor.Value.HasValue ? (float)Math.Round(sensor.Value.Value, 1) : 0;
                                    }
                                }
                            }

                            break;
                        }

                    case HardwareType.HDD:
                        {
                            hardwareItem.Update();
                            Storage storage = new Storage();
                            storage.Name = hardwareItem.Name;
                            foreach (IHardware subHardware in hardwareItem.SubHardware)
                                subHardware.Update();

                            foreach (var sensor in hardwareItem.Sensors)
                            {
                                if (sensor.SensorType == SensorType.Load)
                                {
                                    storage.UsagePercent = sensor.Value ?? 0;
                                }
                                else if (sensor.SensorType == SensorType.Data)
                                {
                                    storage.Usage = sensor.Value.HasValue ? (float)Math.Round(sensor.Value.Value, 1) : 0;
                                }
                                else if (sensor.SensorType == SensorType.Temperature)
                                {
                                    if (sensor.Value != null) storage.Temperature = (int)sensor.Value.Value;
                                }
                            }
                            data.Storages.Add(storage);
                            break;
                        }

                    case HardwareType.GpuNvidia:
                        {
                            hardwareItem.Update();
                            Gpu gpu = new Gpu();
                            gpu.Name = hardwareItem.Name;
                            foreach (IHardware subHardware in hardwareItem.SubHardware)
                            {
                                subHardware.Update();
                            }

                            foreach (var sensor in hardwareItem.Sensors)
                            {
                                if (sensor.SensorType == SensorType.Load)
                                {
                                    if (sensor.Name.Contains("Core"))
                                    {
                                        gpu.Load = sensor.Value.HasValue ? (float)Math.Round(sensor.Value.Value, 1) : 0;
                                    }
                                }
                                else if (sensor.SensorType == SensorType.Data)
                                {
                                    if (sensor.Name.Contains("Free"))
                                    {
                                        gpu.GpuMemoryFree = sensor.Value.HasValue ? (int)sensor.Value.Value : 0;
                                    }else if(sensor.Name.Contains("Used"))
                                    {
                                        gpu.GpuMemoryUsed = sensor.Value.HasValue ? (int)sensor.Value.Value : 0;
                                    }
                                    else if (sensor.Name.Contains("Total"))
                                    {
                                        gpu.GpuMemoryTotal = sensor.Value.HasValue ? (int)sensor.Value.Value : 0;
                                    }
                                }
                                else if (sensor.SensorType == SensorType.Temperature)
                                {
                                    if (sensor.Name.Contains("Core"))
                                    {
                                        gpu.Temperature = sensor.Value.HasValue ? (float)Math.Round(sensor.Value.Value, 1) : 0;
                                    }
                                }else if (sensor.SensorType == SensorType.Clock)
                                {
                                    if (sensor.Name.Contains("Core"))
                                    {
                                        gpu.Clock = sensor.Value.HasValue ? (int)sensor.Value.Value : 0;
                                    }
                                }
                            }
                            data.Gpus.Add(gpu);


                            break;
                        }
                    }
            }
            return data;
        }
    }
}
