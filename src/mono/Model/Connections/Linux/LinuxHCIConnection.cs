
using System;

namespace monotooth.Model.Connections
{
	
	
	public class LinuxHCIConnection : HCIConnection
	{
		
		public LinuxHCIConnection()
		{
		}
		private int sockf;
		public int SocketDescriptor
		{
			get
			{
				return sockf;
			}
		}
		private monotooth.Model.BluetoothAddress fromaddr;
		public monotooth.Model.BluetoothAddress from
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
		private monotooth.Model.BluetoothAddress toaddr;
		public monotooth.Model.BluetoothAddress to
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
