
using System;

namespace monotooth.Model.Connections
{
	
	/// <summary>The superinterface that all connections must use.</summary>
	public interface IConnection
	{
		/// <summary>Connects the connections end-points together.</summary>
		void connect();
		/// <summary>Disconnects the connection.</summary>
		void disconnect();
		/// <summary>Tells wether the connection is open or not.</summary>
		/// <returns>A bool indicating the connection status</returns>
		bool isConnected();
		/// <summary>Writes bytes stored to a <c>System.Text.StringBuilder</c> onto the connection.</summary>
		/// <param name="bytes">The bytes to write to connection, wrapped in <c>System.Text.StringBuilder</c>.</param>
		void Write(System.Text.StringBuilder bytes);
		/// <summary>Reads bytes to a <c>System.Text.StringBuilder</c> from the connection.</summary>
		/// <param name="bytes">Bytes to be read from connection, wrapped in <c>System.Text.StringBuilder</c>.</param>
		void Read(System.Text.StringBuilder bytes);		
		/// <summary>An optional property that stores the current socket descriptor.</summary>
		/// <value>The socket descriptor that is a part of the connection. </value>
		int SocketDescriptor
		{
			get;			
		}
		/// <summary>Bluetooth address that describes the from-part of the connection. </summary>
		/// <value>A <c>BluetoothAddress</c> with the from-part of the connection.</value>
		monotooth.Model.BluetoothAddress from
		{
			get;		
			set;
		}
		/// <summary>Bluetooth address that describes the to-part of the connection. </summary>
		/// <value>A <c>BluetoothAddress</c> with the to-part of the connection.</value>	
		monotooth.Model.BluetoothAddress to
		{
			get;
			set;
		}
		
	}
}
