
using System;
using System.Runtime.InteropServices;

namespace monotooth.Connections
{
	
	
	public class LinuxServiceConnection : ServiceConnection 
	{
		
		private LinuxServiceConnection()
		{
		}
		public LinuxServiceConnection(monotooth.Connections.RFCommConnection conn)
		{
			this.usedconn = conn;
		}
		private monotooth.Connections.RFCommConnection usedconn;
		public monotooth.Connections.RFCommConnection Connection
		{
			get
			{
				return usedconn;
			}
		}
		private IntPtr session;		
		public void connect()
		{
		}
		public void connect(uint uuid)
		{			
			monotooth.Device.DeviceFactory fac = monotooth.Device.DeviceFactory.GetFactory();
			monotooth.Device.ILocalDevice localdev = fac.CreateLocalDevice();
			monotooth.Device.DevicePool devpool = localdev.Inquire();
			foreach(monotooth.Device.IRemoteDevice remotedev in devpool)
			{
			monotooth.Service.ServicePool servpool = remotedev.InquireServices(uuid);
			monotooth.Connections.RFCommConnectionFactory rfcommfac = monotooth.Connections.RFCommConnectionFactory.GetFactory();
			monotooth.Connections.RFCommConnection conn =  rfcommfac.CreateRFCommConnection(localdev.Address,remotedev.Address);
			if(servpool.Count>0)
			{			
				this.usedconn = conn;
				this.usedconn.Connected = true;
				this.usedconn.connect(servpool[0].rfcomm_port);
			} 
			}
		}
		~LinuxServiceConnection()
		{
			this.usedconn.disconnect();
			sdp_close(this.session);
		}
		public void disconnect()
		{
		}
		private bool isconnected = false;
		public void Write(System.Text.StringBuilder bld)
		{
			if(this.usedconn.Connected)
			{
			this.usedconn.Write(bld);
			}
		}
		public void Read(System.Text.StringBuilder bld)
		{
			if(this.usedconn.Connected)
			{
			this.usedconn.Read(bld);
			}
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
		public int SocketDescriptor
		{
			get
			{
				return this.usedconn.SocketDescriptor;
			}
		}		
		public monotooth.BluetoothAddress from
		{
			get
			{
				return this.usedconn.from;
			}
			set
			{
				this.usedconn.from = value;
			}
			
		}		
		
		public monotooth.BluetoothAddress to
		{
			get
			{
				return this.usedconn.to;
			}
			set
			{
				this.usedconn.to = value;
			}
		}
		
	}
}
