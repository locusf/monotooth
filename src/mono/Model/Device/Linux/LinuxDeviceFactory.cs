
using System;

namespace monotooth.Model.Device
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
