
using System;

namespace monotooth.Model.Device
{
	
	
	public class LinuxDeviceFactory : DeviceFactory
	{
		
		public LinuxDeviceFactory()
		{
		}
		public override IDevice CreateDevice()
		{
			return new LinuxDevice();
		}
	}
}
