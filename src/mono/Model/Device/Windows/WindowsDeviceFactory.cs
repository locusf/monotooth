
using System;

namespace monotooth.Model.Device
{
	
	
	public class WindowsDeviceFactory : DeviceFactory
	{
		
		public WindowsDeviceFactory()
		{
		}
		public override IDevice CreateDevice()
		{
			return new WindowsDevice();
		}
		
	}
}
