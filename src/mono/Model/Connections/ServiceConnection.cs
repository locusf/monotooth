
using System;

namespace monotooth.Model.Connections
{
	/// <summary>A superinterface for service-oriented connections.</summary>
	/// <remarks><para>This interface only has service registration as a feature.</para>
	/// <para>To connect to a specific service, first inquire devices,
	/// then use InquireServices with an UUID to connect to this specific service</para></remarks>
	
	public interface ServiceConnection : IConnection
	{
		/// <summary>Connects this connection to a specific service.
		/// <example>
		/// <code lang="C#">
		
		/// </code>
		/// </example>
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
	}
}
