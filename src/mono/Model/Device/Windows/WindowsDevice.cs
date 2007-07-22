
using System;

namespace monotooth.Model.Device
{
	
	
	public class WindowsDevice : ILocalDevice
	{
		
		public WindowsDevice()
		{
		}
		// Instance variables
		private monotooth.Model.BluetoothAddress address;
		private string name;
		private monotooth.Model.Connections.IConnection conn;
		private monotooth.Model.Service.ServicePool services;
		// Implemented properties from IDevice
		public monotooth.Model.BluetoothAddress Address 
		{
			get { return this.address; }
			set { this.address = value; }
		}
		public string FriendlyName
		{
			get { return this.name; }
			set { this.name = value; }
		}
		public monotooth.Model.Connections.IConnection Connection
		{
			get { return this.conn; }
			set { this.conn = value;}
		}
		public monotooth.Model.Service.ServicePool Services
		{
			get { return this.services; }
			set { this.services = value; }
		}
		// Implemented function from IDevice
		public DevicePool Inquire()
		{
			return null;
		}
		public monotooth.Model.Service.ServicePool InquireServices(IDevice dev, uint uuid)
		{
			return null;
		}
		public string AddressAsString()
		{
			return "";
		}
		public monotooth.Model.BluetoothAddress StringAsAddress(string addr)
		{
			return null;
		}
	}
}
