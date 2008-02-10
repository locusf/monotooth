
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
			monotooth.BluetoothAddress ba = monotooth.Device.LinuxDevice.StringAsAddressStatic(testaddress);
			string result = monotooth.Device.LinuxDevice.AddressAsString(ba);
			Console.WriteLine(result);
			Assert.IsTrue(result.CompareTo(testaddress)==0);
			testaddress = "this:is:something:not:an:address";
			ba = monotooth.Device.LinuxDevice.StringAsAddressStatic(testaddress);
			result = monotooth.Device.LinuxDevice.AddressAsString(ba);
			Assert.IsFalse(result.CompareTo(testaddress)==0);
		}
		[Test]
		public void TestFactoriesAndProducts()
		{
			monotooth.Device.DeviceFactory fac = monotooth.Device.DeviceFactory.GetFactory();
			Assert.IsNotNull(fac);
			monotooth.Device.ILocalDevice dev = fac.CreateLocalDevice();
			Assert.IsNotNull(dev);
			monotooth.Connections.RFCommConnectionFactory rfcommfac = monotooth.Connections.RFCommConnectionFactory.GetFactory();
			Assert.IsNotNull(rfcommfac);
			monotooth.Connections.RFCommConnection rfcommconn = rfcommfac.CreateRFCommConnection(new monotooth.BluetoothAddress(), new monotooth.BluetoothAddress());
			Assert.IsNotNull(rfcommconn);
			monotooth.Connections.ServiceConnectionFactory servfac = monotooth.Connections.ServiceConnectionFactory.GetFactory();
			Assert.IsNotNull(servfac);
			monotooth.Connections.ServiceConnection servconn = servfac.CreateServiceConnection(rfcommconn);
			Assert.IsNotNull(servconn);
		}
		[Test]
		public void TestLocalDeviceInformations()
		{
			monotooth.Device.DeviceFactory fac = monotooth.Device.DeviceFactory.GetFactory();			
			monotooth.Device.ILocalDevice dev = fac.CreateLocalDevice();
			Assert.IsNotNull(dev.Address);
			Assert.IsNotNull(dev.FriendlyName);		
		}
		[Test]
		[Ignore("Remove this ignoration if you have more than 1 device in the piconet.")]
		public void TestInquiryOfDevices()
		{
			monotooth.Device.DeviceFactory fac = monotooth.Device.DeviceFactory.GetFactory();			
			monotooth.Device.ILocalDevice dev = fac.CreateLocalDevice();
			monotooth.Device.DevicePool pool = dev.Inquire();
			Assert.IsNotNull(pool[0]);
		}
		[Test]
		[Ignore("Remove this ignoration if you have more than 1 device in the piconet and these devices have services registered.")]
		public void TestServiceInquiry()
		{
			monotooth.Device.DeviceFactory fac = monotooth.Device.DeviceFactory.GetFactory();			
			monotooth.Device.ILocalDevice dev = fac.CreateLocalDevice();
			monotooth.Device.DevicePool pool = dev.Inquire();
			monotooth.Service.ServicePool servpool = pool[0].InquireServices(0);
			Assert.IsNotNull(servpool[0]);
		}
	}
}
