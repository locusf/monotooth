
using System;

namespace monotooth.Connections
{
	
	
	public class LinuxHCIConnection : HCIConnection
	{
		
		public LinuxHCIConnection()
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
		private bool enc = false;
		public bool Encryption
		{
			get
			{
				return enc;
			}
			set
			{
				enc = value;
			}
			
		}
		private bool auth = false;
		public bool Authentication
		{
			get
			{
				return auth;
			}
			set
			{
				auth = value;
			}
		}
		private bool role = false; 
		public bool Role
		{
			get
			{
				return role;
			}
			set
			{
				this.role = value;
			}
		}
		public void connect()
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
	}
}
