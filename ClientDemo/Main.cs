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
			monotooth.Model.Device.ILocalDevice devi = fac.CreateLocalDevice();
			Console.WriteLine("Information of the local device: ");
			Console.WriteLine("Name: "+devi.FriendlyName+ " Address: "+ devi.AddressAsString());
			Console.WriteLine("Searching for devices...");
			monotooth.Model.Device.DevicePool pool = devi.Inquire();						
			foreach(monotooth.Model.Device.IRemoteDevice dev in pool)
			{
				Console.WriteLine("Name: "+ dev.FriendlyName + " Address:"+ dev.AddressAsString()+"\n Now searching for services..");
				dev.Services = dev.InquireServices((uint)0x0);
				Console.WriteLine("Services in :"+dev.FriendlyName);
				foreach(monotooth.Model.Service.Service serv in dev.Services)
				{
					Console.WriteLine("Service name: "+serv.name+"\nService description: "+serv.description+"\nService Port: "+serv.rfcomm_port);									
				}
			}
			monotooth.Model.Connections.RFCommConnectionFactory connfac = monotooth.Model.Connections.RFCommConnectionFactory.GetFactory();
			monotooth.Model.Connections.RFCommConnection conn = connfac.CreateRFCommConnection(devi.Address,devi.Address);
			monotooth.Model.Connections.ServiceConnectionFactory servfac = monotooth.Model.Connections.ServiceConnectionFactory.GetFactory();
			monotooth.Model.Connections.ServiceConnection servconn = servfac.CreateServiceConnection(conn);	
			servconn.connect((uint)0xABCD);
			System.Text.StringBuilder bld = new System.Text.StringBuilder("Hello there!");
			servconn.Write(bld);
			bld = new System.Text.StringBuilder();
			servconn.Read(bld);
			Console.WriteLine(bld.ToString());
			}
	}
}