
using System;

namespace monotooth.Model.Device
{
	
	
	public interface IRemoteDevice : IDevice
	{				
		/// <summary>The services that this device offers. </summary>
		/// <value>A pool of services. </value>
		monotooth.Model.Service.ServicePool Services
		{
			get;
			set;
		}
		/// <summary>Inquires services from the this device, then adding them to service pool.</summary>		
		/// <param name="uuid">The uuid of the service to search for, to search all services, set this parameter to 0.</param>
		/// <returns>A pool of services, see <see cref="M:monotooth.Model.Service.ServicePool">ServicePool</see>.</returns>
		monotooth.Model.Service.ServicePool InquireServices(uint uuid);
	}
}
