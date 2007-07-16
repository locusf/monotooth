
using System;

namespace monotooth.Model.Connections
{
	
	/// <summary>The superinterface for RFCOMM connections. </summary>
	public interface RFCommConnection : IConnection
	{
		/// <summary>Connects this connection. A RFCOMM channel is also specified. </summary>
		/// <param name="channel">The RFCOMM channel to connect to.</param>
		/// <remarks>The connection()-method described in <c>IConnection</c> makes the port default to 0.</remarks>
		void connect(int channel);
		/// <summary>Listens for a RFCOMM connection in the specified channel. </summary>
		/// <param name="channel">The RFCOMM channel to listen for. </param>
		void listen(int channel);
		/// <summary>Listens for a RFCOMM connection in the specified channel and maximum connections. </summary>
		/// <param name="channel">The RFCOMM channel to listen to. </param>	
		/// <param name="maxconns">Number of connections to listen.</param>
		void listen(int channel, int maxconns);
		/// <summary>Tells wether this connection is connected or not. </summary>
		/// <value>true/false</value>
		bool Connected
		{
			get;
			set;
		}
		/// <summary>Gives the current channel for the connection, it doesn't matter wether this channel is being listened or connected to.</summary>
		/// <value>An integer between 1-31.</value>
		int Channel
		{
			get;
			set;
		}
	}
}
