
using System;
using NUnit.Framework;

namespace Testing
{
	
	
	[TestFixture()]
	public class LinuxImplementationTest
	{
		[Test]
		public void TestSettingOfSpecificImplementation()
		{
			
		}
		[Test]
		public void TestInquiryOfDevices()
		{
			monotooth.Model.Device.DeviceFactory fac = monotooth.Model.Device.DeviceFactory.GetFactory();
			monotooth.Model.Device.IDevice dev = fac.CreateDevice();
			monotooth.Model.Device.DevicePool pool = dev.Inquire();
			Assert.IsTrue(pool.Count!=0);
		}
		[Test]
		public void TestServiceDiscovery()
		{
		}		
	}
}
