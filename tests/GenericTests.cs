/*
 * Created by SharpDevelop.
 * User: LoCusF
 * Date: 20.4.2008
 * Time: 22:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using NUnit.Framework;
using System;


namespace tests
{
	[TestFixture]
	public class GenericTests
	{
		[Test]
		[ExpectedException(typeof(Exception))]
		public void TestNonPlugged()
		{
			monotooth.Device.ILocalDevice dev = monotooth.Device.LocalDevice.Default;
			dev.Inquire();
			System.Console.WriteLine("Device ");
		}
		
		[TestFixtureSetUp]
		public void Init()
		{
			// TODO: Add Init code.
		}
		
		[TestFixtureTearDown]
		public void Dispose()
		{
			// TODO: Add tear down code.
		}
	}
}
