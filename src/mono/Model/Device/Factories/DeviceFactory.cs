
using System;

namespace monotooth.Device
{
	
	/// <summary> An abstract factory to create OS-independend devices. </summary>
	public abstract class DeviceFactory
	{
		/// <summary>Returns the factory by the information of the operating system. </summary>
		/// <returns>A device factory. </returns>
		public static DeviceFactory GetFactory()
		{
			int p = (int) System.Environment.OSVersion.Platform;
			if (( p == 128) || (p == 4))
			{	
				return new LinuxDeviceFactory();
			} else 
			{
				return new WindowsDeviceFactory();
			}
		}
		/// <summary>An abstract method to create a local device. The sub-class factories must implement this method.</summary>
		/// <returns>A local device.</returns>
		public abstract ILocalDevice CreateLocalDevice();		
	}
}
