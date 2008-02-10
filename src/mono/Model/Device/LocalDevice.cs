
using System;

namespace monotooth.Device
{
	
	
	public class LocalDevice
	{
		public static ILocalDevice Default
		{
			get
			{
			return DeviceFactory.GetFactory().CreateLocalDevice();
			}
		}
	}
}
