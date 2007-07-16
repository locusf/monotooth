
using System;

namespace monotooth.Model.Device
{
	
	/// <summary>The superinterface for all devices. The sole purpose for this
	/// interface is to offer as much information as possible for the means 
	/// of using devices.</summary>
	public interface IDevice
	{
		/// <summary>The devices address. </summary>
		/// <value>A 48-bit bluetooth address. </value>
		monotooth.Model.BluetoothAddress Address 
		{
			get;
			set;
		}
		/// <summary>The friendly name of this device (the human readable form). </summary>
		/// <value>A string. </value>
		string FriendlyName
		{
			get;
			set;
		}				
		/// <summary>The services that this device offers. </summary>
		/// <value>A pool of services. </value>
		monotooth.Model.Service.ServicePool Services
		{
			get;
			set;
		}
		/// <summary> This method searches the surrounding area for devices,
		/// and then adds them to a device pool.</summary>
		/// <returns>A pool of devices, see <see cref="M:monotooth.Model.Device.DevicePool">DevicePool</see>.</returns>
		DevicePool Inquire();
		/// <summary>Returns the 48-bit bluetooth address of this device as a string .</summary>
		/// <returns>String presentation of this devices bluetooth address.</returns>
		string AddressAsString();
		/// <summary>Inquires services from the specified device, then adding them to service pool.</summary>
		/// <param name="dev">The device to search services from.</param>
		/// <param name="uuid">The uuid of the service to search for, to search all services, set this parameter to 0.</param>
		/// <returns>A pool of services, see <see cref="M:monotooth.Model.Service.ServicePool">ServicePool</see>.</returns>
		monotooth.Model.Service.ServicePool InquireServices(IDevice dev, uint uuid);
		/// <summary>Converts a string presentation of a bluetooth address to a <c>BluetoothAddress</c>.</summary>
		/// <param name="addr"> A bluetooth address as string. </param>
		/// <returns>A bluetooth address wrapped to a class. </returns>
		monotooth.Model.BluetoothAddress StringAsAddress(string addr);
	}
}
