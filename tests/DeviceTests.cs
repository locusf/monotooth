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
			
			Assert.IsNotEmpty(pool, "There are some devices in this pool if there are any devices around the bluetooth device");
		}
		[Test]		
		public void TestStringToAddress()
		{
			Assert.IsNotNull(dev.StringAsAddress("00:00:00:00:00:00"),"Should not return null");			
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
