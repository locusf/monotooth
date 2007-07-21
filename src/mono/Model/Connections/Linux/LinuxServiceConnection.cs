
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
		public void connect(uint uuid)
		{			
			monotooth.Model.Device.DeviceFactory fac = monotooth.Model.Device.DeviceFactory.GetFactory();
			monotooth.Model.Device.IDevice localdev = fac.CreateDevice();
			monotooth.Model.Device.DevicePool devpool = localdev.Inquire();
			foreach(monotooth.Model.Device.IDevice remotedev in devpool)
			{
			monotooth.Model.Service.ServicePool servpool = localdev.InquireServices(remotedev,uuid);
			monotooth.Model.Connections.RFCommConnectionFactory rfcommfac = monotooth.Model.Connections.RFCommConnectionFactory.GetFactory();
			monotooth.Model.Connections.RFCommConnection conn =  rfcommfac.CreateRFCommConnection(localdev.Address,remotedev.Address);
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
		private bool isconnected;
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
		public monotooth.Model.BluetoothAddress from
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
		
		public monotooth.Model.BluetoothAddress to
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
