
using System;

namespace monotooth.Device
{
	
	
	public class LinuxDeviceFactory : DeviceFactory
	{
		
		public LinuxDeviceFactory()
		{
		}
		public override ILocalDevice CreateLocalDevice()
		{
			return new LinuxDevice();
		}
	}
}
