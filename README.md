# PlayEssential.Plugins.HardwareMonitor


PlayEssential.Services.Plugin의 인터페이스를 이용한 HardwareMonitor의 구현입니다.

Json형태로 EventBridge에 "HardwareMonitor" 으로 Publish됩니다.
관리자 권한이 없다면 Cpu 온도를 측정할 수 없으며 Amd gpu가 없는 관계로 Nvidia gpu만 구현되어 있습니다.
