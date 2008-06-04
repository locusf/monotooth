/*
 * Created by SharpDevelop.
 * User: LoCusF
 * Date: 27.5.2008
 * Time: 10:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using NUnit.Framework;
using System;

namespace tests
{
	[TestFixture]
	public class DeviceTests
	{
		private monotooth.Device.ILocalDevice dev;
		[Test]
		public void TestInquiry()
		{			
			monotooth.Device.DevicePool pool = dev.Inquire();
			
			Assert.IsNotEmpty(pool, "If this test fails then there are no bluetooth devices around or they are hidden. Or then inquiry doesn't work :)");
			foreach (monotooth.Device.IRemoteDevice rdev in pool)
			{
				Assert.IsNotNull(rdev.Address,"Address could not be fetched from remote device!");
				Assert.IsNotNull(rdev.FriendlyName,"Remote device's friendly name not fetched!");					
			}
		}
		[Test]
		public void TestLocalDeviceInfo()
		{
			Assert.IsNotEmpty(dev.Address.Array, "Local device doesn't have an address!");
			Assert.IsNotEmpty(dev.FriendlyName, "Local device doesn't have a name!");
		}
		[TestFixtureSetUp]
		public void Init()
		{			
			dev = monotooth.Device.LocalDevice.Default;		
		}
		
		[TestFixtureTearDown]
		public void Dispose()
		{		
			dev = null;
		}
	}
}
