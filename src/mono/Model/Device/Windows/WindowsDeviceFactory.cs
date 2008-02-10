
using System;

namespace monotooth.Device
{
	
	
	public class WindowsDeviceFactory : DeviceFactory
	{
		
		public WindowsDeviceFactory()
		{
		}
		public override ILocalDevice CreateLocalDevice()
		{
			return new WindowsDevice();
		}
		
	}
}
