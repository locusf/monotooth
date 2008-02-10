
using System;

namespace monotooth.Device
{
	
	/// <summary>An interface for all remote device implementation.</summary>
	public interface IRemoteDevice : IDevice
	{				
		/// <summary>The services that this device offers. </summary>
		/// <value>A pool of services. </value>
		monotooth.Service.ServicePool Services
		{
			get;
			set;
		}
		/// <summary>Inquires services from this device, then adds them to service pool.</summary>		
		/// <param name="uuid">The uuid of the service to search for, to search all services, set this parameter to 0.</param>
		/// <returns>A pool of services, see <see cref="M:monotooth.Service.ServicePool">ServicePool</see>.</returns>
		monotooth.Service.ServicePool InquireServices(uint uuid);
	}
}
