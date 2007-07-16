// project created on 7/4/2007 at 8:47 PM
using System;
using System.Runtime.InteropServices;
namespace ClientDemo
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			monotooth.Model.Device.DeviceFactory fac = monotooth.Model.Device.DeviceFactory.GetFactory();
			Console.WriteLine((fac is monotooth.Model.Device.LinuxDeviceFactory));
			Console.WriteLine((fac is monotooth.Model.Device.WindowsDeviceFactory));			
			monotooth.Model.Device.IDevice devi = fac.CreateDevice();
			Console.WriteLine("Information of the local device: ");
			Console.WriteLine("Name: "+devi.FriendlyName+ " Address: "+ devi.AddressAsString());
			Console.WriteLine("Searching for devices...");
			monotooth.Model.Device.DevicePool pool = devi.Inquire();						
			foreach(monotooth.Model.Device.IDevice dev in pool)
			{
				Console.WriteLine("Name: "+ dev.FriendlyName + " Address:"+ dev.AddressAsString()+"\n Now searching for services..");
				dev.Services = devi.InquireServices(dev,(uint)0xABCD);
				Console.WriteLine("Services in :"+dev.FriendlyName);
				foreach(monotooth.Model.Device.LinuxDevice.Service serv in dev.Services)
				{
					Console.WriteLine("Service name: "+(new string(serv.name))+"\nService description: "+(new string(serv.description))+"\nService Port: "+serv.rfcomm_port);
					monotooth.Model.Connections.RFCommConnectionFactory connfac = monotooth.Model.Connections.RFCommConnectionFactory.GetFactory();
					monotooth.Model.Connections.RFCommConnection conn = connfac.CreateRFCommConnection(devi.Address,dev.Address);
					conn.connect(serv.rfcomm_port);			
					System.Text.StringBuilder bld = new System.Text.StringBuilder("Hello there!");
					conn.Write(bld);
			
				}
			}			
			}
	}
}