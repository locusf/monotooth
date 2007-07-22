
using System;

namespace monotooth.Model.Device
{
	
	
	public interface ILocalDevice: IDevice
	{
		/// <summary> This method searches the surrounding area for devices,
		/// and then adds them to a device pool.</summary>
		/// <returns>A pool of devices, see <see cref="M:monotooth.Model.Device.DevicePool">DevicePool</see>.</returns>
		DevicePool Inquire();
	}
}
