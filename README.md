# PlayEssential.Plugins.HardwareMonitor


PlayEssential.Services.Plugin의 인터페이스를 이용한 HardwareMonitor의 구현입니다.

Json형태로 EventBridge에 "HardwareMonitor" 으로 Publish됩니다.
관리자 권한이 없다면 Cpu 온도를 측정할 수 없으며 Amd gpu가 없는 관계로 Nvidia gpu만 구현되어 있습니다.

전송되는 정보에는 아래가 있습니다.


* Cpu - CpuName, BusSpeed, PackageTemperature, TotalLoadPercent
  * CpuCores - LoadPercent, Temperature, Clock
* Gpu ( 멀티 gpu가 기본적으로 인식됩니다.) - Name, Clock, LoadPercent, FreeMemory, UsedMemory, TotalMemory, Temperature
* Ram - UsagePercent, Total, Usage
* Storage - Name, UsagePercent, Usage, Temperature


사용된 OpenHardwareMonitorLib는 https://github.com/Asyvix/LibreHardwareMonitor 여기에서 소스코드를 다운받을 수 있으며, MPL2.0라이선스가 적용됩니다.
