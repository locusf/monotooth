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
	public class FactoryTests
	{
		private monotooth.Device.DeviceFactory devfac;
		private monotooth.Connections.RFCommConnectionFactory connfac;
		[Test]
		public void TestDeviceProduct()
		{
			monotooth.Device.ILocalDevice dev = devfac.CreateLocalDevice();
			Assert.IsNotNull(dev);
		}
		
		[Test]
		public void TestConnectionProduct()
		{
			monotooth.Connections.RFCommConnection conn = connfac.CreateRFCommConnection(new monotooth.BluetoothAddress(), new monotooth.BluetoothAddress());
			Assert.IsNotNull(conn);
		}
		
		[TestFixtureSetUp]
		public void Init()
		{
			devfac = monotooth.Device.DeviceFactory.GetFactory();
			connfac = monotooth.Connections.RFCommConnectionFactory.GetFactory();			
		}
		
		[TestFixtureTearDown]
		public void Dispose()
		{
			devfac = null;
			connfac = null;
		}
	}
}
