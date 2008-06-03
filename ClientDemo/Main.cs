// project created on 7/4/2007 at 8:47 PM
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace ClientDemo
{
	class MainClass
	{
		[Serializable()]
		private class DemoClass: ISerializable
		{			 
			 public int jee;
			 public string joo;
			 public DemoClass()
			 {
			 	jee = 0;
			 	joo = "joo";
			 }
			 public DemoClass(SerializationInfo info, StreamingContext ctxt)
			 {
			 	jee = (int)info.GetValue("jee", typeof(int));
			 	joo = (string)info.GetValue("joo",typeof(string));
			 }
			 public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
			 {
			 info.AddValue("jee", jee);
			 info.AddValue("joo", joo);
			 }
		}
		public static void Main(string[] args)
		{
			monotooth.Device.ILocalDevice devi = monotooth.Device.LocalDevice.Default;			
			monotooth.Device.DevicePool pool = devi.Inquire();	
			monotooth.BluetoothAddress addr = new monotooth.BluetoothAddress("00:01:02:03:04:05");
			foreach(monotooth.Device.IRemoteDevice dev in pool)
			{
				Console.WriteLine("Name: "+ dev.FriendlyName + " Address:"+ monotooth.BluetoothAddress.AddressAsString(dev.Address)+"\n Now searching for services..");				
				Console.WriteLine("Services in :"+dev.FriendlyName);
			}
			/*
			monotooth.Device.DeviceFactory fac = monotooth.Device.DeviceFactory.GetFactory();
			Console.WriteLine((fac is monotooth.Device.LinuxDeviceFactory));
			Console.WriteLine((fac is monotooth.Device.WindowsDeviceFactory));			
			monotooth.Device.ILocalDevice devi = fac.CreateLocalDevice();
			Console.WriteLine("Information of the local device: ");
			Console.WriteLine("Name: "+devi.FriendlyName+ " Address: "+ devi.AddressAsString());
			Console.WriteLine("Searching for devices...");
			monotooth.Device.DevicePool pool = devi.Inquire();						
			foreach(monotooth.Device.IRemoteDevice dev in pool)
			{
				Console.WriteLine("Name: "+ dev.FriendlyName + " Address:"+ dev.AddressAsString()+"\n Now searching for services..");
				dev.Services = dev.InquireServices((uint)0x0);
				Console.WriteLine("Services in :"+dev.FriendlyName);
				foreach(monotooth.Service.Service serv in dev.Services)
				{
					Console.WriteLine("Service name: "+serv.name+"\nService description: "+serv.description+"\nService Port: "+serv.rfcomm_port);									
				}
			}
			monotooth.Connections.RFCommConnectionFactory connfac = monotooth.Connections.RFCommConnectionFactory.GetFactory();
			monotooth.Connections.RFCommConnection conn = connfac.CreateRFCommConnection(devi.Address,devi.Address);
			monotooth.Connections.ServiceConnectionFactory servfac = monotooth.Connections.ServiceConnectionFactory.GetFactory();
			monotooth.Connections.ServiceConnection servconn = servfac.CreateServiceConnection(conn);	
			servconn.connect((uint)0xABCD);
			System.Text.StringBuilder bld = new System.Text.StringBuilder("Hello there!");
			servconn.Write(bld);
			bld = new System.Text.StringBuilder();
			bld.Capacity = 1024;
			servconn.Read(bld);
			Console.WriteLine(bld.ToString());			
			monotooth.Socket.BluetoothStream bs = new monotooth.Socket.BluetoothStream(servconn.Connection);						
			BinaryFormatter bin = new BinaryFormatter();
			System.String s = "Hello serialized bluetooth stream!";
			bin.Serialize(bs, s);
			bs.Close();			*/
			}
	}
}
