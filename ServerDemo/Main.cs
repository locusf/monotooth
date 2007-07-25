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
			monotooth.Model.Device.DeviceFactory fac = monotooth.Model.Device.DeviceFactory.GetFactory();
			Console.WriteLine((fac is monotooth.Model.Device.LinuxDeviceFactory));
			Console.WriteLine((fac is monotooth.Model.Device.WindowsDeviceFactory));			
			monotooth.Model.Device.ILocalDevice devi = fac.CreateLocalDevice();
			Console.WriteLine("Information of the local device: ");
			Console.WriteLine("Name: "+devi.FriendlyName+ " Address: "+ devi.AddressAsString());
			monotooth.Model.Connections.RFCommConnectionFactory connfac = monotooth.Model.Connections.RFCommConnectionFactory.GetFactory();
			monotooth.Model.Connections.RFCommConnection conn = connfac.CreateRFCommConnection(devi.Address,devi.Address);
			monotooth.Model.Connections.ServiceConnectionFactory servfac = monotooth.Model.Connections.ServiceConnectionFactory.GetFactory();
			monotooth.Model.Connections.ServiceConnection servconn = servfac.CreateServiceConnection(conn);
			uint uuid = (uint)0xABCD;
			Console.WriteLine(uuid);
			System.Text.StringBuilder bld = new System.Text.StringBuilder();
			servconn.RegisterService("Testing service","Testing service","Testing service",13,uuid);
			servconn.Read(bld);
			bld = new System.Text.StringBuilder("Hello to you too there!");
			servconn.Write(bld);
			try{
			monotooth.Model.Socket.BluetoothStream bs = new monotooth.Model.Socket.BluetoothStream(servconn.Connection);
			BinaryFormatter bin = new BinaryFormatter();
			DemoClass demo = new DemoClass();
			Console.WriteLine(((DemoClass)bin.Deserialize(bs)).joo);
			demo.joo = "Response, serialized.";
			bin.Serialize(bs,demo);
			bs.Close();
			} catch(SerializationException se)
			{
				se.Equals(se);
			}
		}
	}
}