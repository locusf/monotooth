
using System;
using NUnit.Framework;

namespace Testing
{
	
	
	[TestFixture()]
	public class LinuxTest
	{
		[Test]
		public void TestBluetoothAddress()
		{
			string testaddress = "01:02:03:04:05:06";
			Console.WriteLine(testaddress);
			monotooth.Model.BluetoothAddress ba = monotooth.Model.Device.LinuxDevice.StringAsAddressStatic(testaddress);
			string result = monotooth.Model.Device.LinuxDevice.AddressAsString(ba);
			Console.WriteLine(result);
			Assert.IsTrue(result.CompareTo(testaddress)==0);
			testaddress = "this:is:something:not:an:address";
			ba = monotooth.Model.Device.LinuxDevice.StringAsAddressStatic(testaddress);
			result = monotooth.Model.Device.LinuxDevice.AddressAsString(ba);
			Assert.IsFalse(result.CompareTo(testaddress)==0);
		}
		[Test]
		public void TestFactoriesAndProducts()
		{
			monotooth.Model.Device.DeviceFactory fac = monotooth.Model.Device.DeviceFactory.GetFactory();
			Assert.IsNotNull(fac);
			monotooth.Model.Device.ILocalDevice dev = fac.CreateLocalDevice();
			Assert.IsNotNull(dev);
			monotooth.Model.Connections.RFCommConnectionFactory rfcommfac = monotooth.Model.Connections.RFCommConnectionFactory.GetFactory();
			Assert.IsNotNull(rfcommfac);
			monotooth.Model.Connections.RFCommConnection rfcommconn = rfcommfac.CreateRFCommConnection(new monotooth.Model.BluetoothAddress(), new monotooth.Model.BluetoothAddress());
			Assert.IsNotNull(rfcommconn);
			monotooth.Model.Connections.ServiceConnectionFactory servfac = monotooth.Model.Connections.ServiceConnectionFactory.GetFactory();
			Assert.IsNotNull(servfac);
			monotooth.Model.Connections.ServiceConnection servconn = servfac.CreateServiceConnection(rfcommconn);
			Assert.IsNotNull(servconn);
		}
		[Test]
		public void TestLocalDeviceInformations()
		{
			monotooth.Model.Device.DeviceFactory fac = monotooth.Model.Device.DeviceFactory.GetFactory();			
			monotooth.Model.Device.ILocalDevice dev = fac.CreateLocalDevice();
			Assert.IsNotNull(dev.Address);
			Assert.IsNotNull(dev.FriendlyName);		
		}
		[Test]
		[Ignore("Remove this ignoration if you have more than 1 device in the piconet.")]
		public void TestInquiryOfDevices()
		{
			monotooth.Model.Device.DeviceFactory fac = monotooth.Model.Device.DeviceFactory.GetFactory();			
			monotooth.Model.Device.ILocalDevice dev = fac.CreateLocalDevice();
			monotooth.Model.Device.DevicePool pool = dev.Inquire();
			Assert.IsNotNull(pool[0]);
		}
		[Test]
		[Ignore("Remove this ignoration if you have more than 1 device in the piconet and these devices have services registered.")]
		public void TestServiceInquiry()
		{
			monotooth.Model.Device.DeviceFactory fac = monotooth.Model.Device.DeviceFactory.GetFactory();			
			monotooth.Model.Device.ILocalDevice dev = fac.CreateLocalDevice();
			monotooth.Model.Device.DevicePool pool = dev.Inquire();
			monotooth.Model.Service.ServicePool servpool = pool[0].InquireServices(0);
			Assert.IsNotNull(servpool[0]);
		}
	}
}
