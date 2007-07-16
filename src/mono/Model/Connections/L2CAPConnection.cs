
using System;

namespace monotooth.Model.Connections
{
	
	/// <summary> An interface to describe L2CAP connections. </summary>
	public interface L2CAPConnection : IConnection
	{
		/// <summary>Connects to address specified in <see cref="F:monotooth.Model.Connections.IConnection.to">to</see>.</summary>		
		/// <param name="psm">The L2CAP channel to connect to.</param>
		void connect(int psm);
		/// <summary>Listens to L2CAP connection with a channel. </summary>
		/// <param name="psm">The L2CAP channel to listen to. </param>
		void listen(int psm);		
		/// <summary>Listens to L2CAP connection with a channel. </summary>
		/// <param name="psm">The L2CAP channel to listen to. </param>
		/// <param name="maxconns">The number of connections to listen for. </param>
		void listen(int psm, int maxconns);
	}
}
