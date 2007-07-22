
using System;

namespace monotooth.Model.Device
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
