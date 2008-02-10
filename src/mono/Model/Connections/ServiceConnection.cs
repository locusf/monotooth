
using System;

namespace monotooth.Connections
{
	/// <summary>A superinterface for service-oriented connections.</summary>	
	public interface ServiceConnection : IConnection
	{
		/// <summary>Connects this connection to a specific service.		
		/// </summary>
		/// <param name="uuid">The uuid to search for.</param>		
		void connect(uint uuid);
		/// <summary>Registers a service to the local SDP server. This server must be running before any action can be undertaken.</summary>
		/// <param name="name">A name of the service. </param>
		/// <param name="description">Description of the service. </param>
		/// <param name="vendor">Possible vendor for the service. </param>
		/// <param name="rfcomm_channel">The rfcomm channel to listen for service connections. </param>
		/// <param name="uuid">The services UUID as unsigned integer. </param>
		/// <remarks>The UUID of the service is now implemented for simplicity as an unsigned integer.</remarks>
		void RegisterService(string name, string description, string vendor, int rfcomm_channel, uint uuid);
		/// <summary>The connection that was used for this service connection.</summary>
		monotooth.Connections.RFCommConnection Connection
		{
			get;
		}
	}
}
