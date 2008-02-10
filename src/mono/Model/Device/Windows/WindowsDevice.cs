
using System;

namespace monotooth.Device
{
	
	
	public class WindowsDevice : ILocalDevice
	{
		
		public WindowsDevice()
		{
		}
		// Instance variables
		private monotooth.BluetoothAddress address;
		private string name;
		private monotooth.Connections.IConnection conn;
		private monotooth.Service.ServicePool services;
		// Implemented properties from IDevice
		public monotooth.BluetoothAddress Address 
		{
			get { return this.address; }
			set { this.address = value; }
		}
		public string FriendlyName
		{
			get { return this.name; }
			set { this.name = value; }
		}
		public monotooth.Connections.IConnection Connection
		{
			get { return this.conn; }
			set { this.conn = value;}
		}
		public monotooth.Service.ServicePool Services
		{
			get { return this.services; }
			set { this.services = value; }
		}
		// Implemented function from IDevice
		public DevicePool Inquire()
		{
			return null;
		}
		public monotooth.Service.ServicePool InquireServices(IDevice dev, uint uuid)
		{
			return null;
		}
		public string AddressAsString()
		{
			return "";
		}
		public monotooth.BluetoothAddress StringAsAddress(string addr)
		{
			return null;
		}
	}
}
