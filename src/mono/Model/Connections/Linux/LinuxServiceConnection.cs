
using System;
using System.Runtime.InteropServices;

namespace monotooth.Model.Connections
{
	
	
	public class LinuxServiceConnection : ServiceConnection 
	{
		
		private LinuxServiceConnection()
		{
		}
		public LinuxServiceConnection(monotooth.Model.Connections.RFCommConnection conn)
		{
			this.usedconn = conn;
		}
		private monotooth.Model.Connections.RFCommConnection usedconn;
		private IntPtr session;		
		public void connect()
		{
		}		
		~LinuxServiceConnection()
		{
			this.usedconn.disconnect();
			sdp_close(this.session);
		}
		public void disconnect()
		{
		}
		private bool isconnected;
		public void Write(System.Text.StringBuilder bld)
		{
			this.usedconn.Write(bld);
		}
		public void Read(System.Text.StringBuilder bld)
		{
			this.usedconn.Read(bld);
		}
		public bool isConnected()
		{
			return isconnected;
		}
		public void RegisterService(string name, string descr, string vendor, int channel, uint uuid)
		{
				this.session = register_service(name,descr,vendor,channel,uuid);
				this.usedconn.listen(channel);
				this.usedconn.Connected = true;			
		}
		// Native functions
		[DllImport("bluetooth")]
		private static extern int sdp_close(IntPtr session);
		[DllImport("monotooth")]
		private static extern IntPtr register_service(string name, string descr, string vendor, int channel, uint uuid);
		// Interface members
		private int sockf;
		public int SocketDescriptor
		{
			get
			{
				return this.sockf;
			}
		}
		private monotooth.Model.BluetoothAddress fromaddr;
		public monotooth.Model.BluetoothAddress from
		{
			get
			{
				return this.fromaddr;
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
				return this.toaddr;
			}
			set
			{
				this.toaddr = value;
			}
		}
		
	}
}
