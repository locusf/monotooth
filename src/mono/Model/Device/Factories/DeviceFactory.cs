
using System;

namespace monotooth.Model.Device
{
	
	/// <summary> An abstract factory to create OS-independend devices. </summary>
	public abstract class DeviceFactory
	{
		/// <summary>Returns the factory by the information of the operating system. </summary>
		/// <returns>A device factory. </returns>
		public static DeviceFactory GetFactory()
		{
			if ((System.Environment.OSVersion.Platform.value__ == 128) || (System.Environment.OSVersion.Platform.value__ == 4))
			{
				return new LinuxDeviceFactory();
			} else 
			{
				return new WindowsDeviceFactory();
			}
		}
		/// <summary>An abstract method to create devices. The sub-class factories must implement this method.</summary>
		/// <returns>A device.</returns>
		public abstract IDevice CreateDevice();
	}
}
