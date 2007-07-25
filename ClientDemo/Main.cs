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
			try{
			monotooth.Model.Socket.BluetoothStream bs = new monotooth.Model.Socket.BluetoothStream(servconn.Connection);
			DemoClass demo = new DemoClass();
			demo.joo = "Hello via bluetooth stream!";
			BinaryFormatter bin = new BinaryFormatter();
			bin.Serialize(bs, demo);
			Console.WriteLine(((DemoClass)bin.Deserialize(bs)).joo);
			bs.Close();
			} catch(SerializationException se)
			{
				se.Equals(se);
			}
			}
	}
}