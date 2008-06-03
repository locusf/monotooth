// ServiceTest.cs created with MonoDevelop
// User: locusf at 11:14 AM 6/2/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using NUnit.Framework;

namespace tests
{
	
	
	[TestFixture()]
	public class ServiceTest
	{
		private monotooth.Device.ILocalDevice dev;
		private monotooth.Device.DevicePool pool;
		[Test()]
		public void TestServiceInquiry()
		{
			Assert.IsNotEmpty(pool,"There should be devices around!");
			foreach (monotooth.Device.IRemoteDevice rdev in pool)
			{
				Assert.IsNotEmpty(rdev.FriendlyName,"Could not get the name of this remote device!");
				Assert.IsNotEmpty(rdev.Address.b, "Could not get the address of this remote device!");
				monotooth.Service.ServicePool spool = rdev.InquireServices((uint)0x0);
				foreach (monotooth.Service.Service serv in spool)
				{
					// Not all services return a description
					// Assert.IsNotEmpty(serv.description,"Could not get a description for this service!");
					Assert.IsNotEmpty(serv.name,"Could not get a name for the service!");
					Assert.IsTrue((serv.rfcomm_port > 0 && serv.rfcomm_port < 21),"Could not get an rfcomm port for this service!");
				}
			}
		}
		
		[TestFixtureSetUp]
		public void Init()
		{			
			dev = monotooth.Device.LocalDevice.Default;
			pool = dev.Inquire();
		}
		
		[TestFixtureTearDown]
		public void Dispose()
		{		
			dev = null;
			pool.Clear();
		}
	}
}
