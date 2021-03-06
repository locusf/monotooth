
using System;

namespace monotooth.Connections
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
		/// <summary>Read bytes to IntPtr with offset. </summary>
		/// <param name="onebyte">A pointer that describes a byte array to read from socket.</param>
		/// <param name="offset">Offset of the data.</param>
		/// <param name="count">The number of bytes to read.</param>
		void ReadWithOffset(IntPtr onebyte, int offset, int count);
		/// <summary>Write bytes from IntPtr with offset. </summary>
		/// <param name="onebyte">A pointer that describes a byte array to write to socket.</param>
		/// <param name="offset">Offset of the data.</param>
		/// <param name="count">The number of bytes to read.</param>		
		void WriteWithOffset(IntPtr onebyte, int offset, int count);
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
		/// <summary>A read-only value that describes the number last read/written bytes.</summary>
		/// <value>An integer describing the number of bytes last written/read. </value>
		int BytesUsed
		{
			get;
		}
	}
}
