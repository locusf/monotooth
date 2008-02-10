// project created on 7/4/2007 at 8:47 PM
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
namespace ServerDemo
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
			monotooth.Device.DeviceFactory fac = monotooth.Device.DeviceFactory.GetFactory();
			Console.WriteLine((fac is monotooth.Device.LinuxDeviceFactory));
			Console.WriteLine((fac is monotooth.Device.WindowsDeviceFactory));			
			monotooth.Device.ILocalDevice devi = fac.CreateLocalDevice();
			Console.WriteLine("Information of the local device: ");
			Console.WriteLine("Name: "+devi.FriendlyName+ " Address: "+ devi.AddressAsString());
			monotooth.Connections.RFCommConnectionFactory connfac = monotooth.Connections.RFCommConnectionFactory.GetFactory();
			monotooth.Connections.RFCommConnection conn = connfac.CreateRFCommConnection(devi.Address,devi.Address);
			monotooth.Connections.ServiceConnectionFactory servfac = monotooth.Connections.ServiceConnectionFactory.GetFactory();
			monotooth.Connections.ServiceConnection servconn = servfac.CreateServiceConnection(conn);
			uint uuid = (uint)0xABCD;
			Console.WriteLine(uuid);
			System.Text.StringBuilder bld = new System.Text.StringBuilder();
			bld.Capacity = 1024;
			servconn.RegisterService("Testing service","Testing service","Testing service",13,uuid);
			servconn.Read(bld);
			bld = new System.Text.StringBuilder("Hello to you too there!");
			servconn.Write(bld);			
			monotooth.Socket.BluetoothStream bs = new monotooth.Socket.BluetoothStream(servconn.Connection);
			BinaryFormatter bin = new BinaryFormatter();			
			System.String ret = (System.String)bin.Deserialize(bs);
			Console.WriteLine(ret);
			bs.Close();			
		}
	}
}