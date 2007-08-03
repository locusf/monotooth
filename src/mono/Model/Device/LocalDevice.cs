
using System;

namespace monotooth.Model.Device
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
