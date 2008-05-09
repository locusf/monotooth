
using System;

namespace monotooth.Connections
{
	
	
	public class WindowsL2CAPConnection : L2CAPConnection
	{
		
		public WindowsL2CAPConnection()
		{
		}
		private int sockf = 0;
		public int SocketDescriptor
		{
			get
			{
				return sockf;
			}
		}
		private monotooth.BluetoothAddress fromaddr;
		public monotooth.BluetoothAddress from
		{
			get
			{
			return fromaddr;
			}			
			set
			{
				this.fromaddr = value;
			}
		}
		private monotooth.BluetoothAddress toaddr;
		public monotooth.BluetoothAddress to
		{
			get
			{
			return toaddr;
			}
			set
			{
			this.toaddr = value;
			}
		}
		public void connect()
		{
		}
		public void connect(int psm)
		{
		}
		public void disconnect()
		{
		}
		public bool isConnected()
		{
			return false;
		}
		public void Write(System.Text.StringBuilder bytes)
		{
		}
		public void Read(System.Text.StringBuilder bytes)
		{
		}
		public void listen(int psm)
		{
		}
		public void listen(int psm, int maxconns)
		{
		}
	}
}
