// AddressTests.cs created with MonoDevelop
// User: locusf at 8:04 PM 6/1/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using NUnit.Framework;

namespace tests
{
	
	
	[TestFixture()]
	public class AddressTests
	{
		
		[Test()]
		public void TestStringAsAddress()
		{
			monotooth.BluetoothAddress ba =	monotooth.BluetoothAddress.StringAsAddress("80:7F:7E:7D:7C:7B");
			byte[] comp = new byte[]{ 128, 127, 126, 125, 124, 123};
			monotooth.BluetoothAddress ba2 = new monotooth.BluetoothAddress();
			monotooth.BluetoothAddress zeroaddr = new monotooth.BluetoothAddress();
			ba2.b = comp;
			Assert.AreNotEqual(ba.b, zeroaddr,"Should not return zero address with good bluetooth address!");
			Assert.AreEqual(ba2.b,ba.b, "Test data comparison failed!");
			monotooth.BluetoothAddress bogusba = monotooth.BluetoothAddress.StringAsAddress("80LOL:FF:RFF:FFF:FFF:FFF:FF");
			Assert.AreEqual(zeroaddr.b, bogusba.b, "Bogus data accepted!");
			monotooth.BluetoothAddress toobigblocksba = monotooth.BluetoothAddress.StringAsAddress("801322F:FF:FF:FFAF:FAF:5FF");
			Assert.AreEqual(zeroaddr.b, toobigblocksba.b,"Data with too big blocks accepted!");
		}
		[Test()]
		public void TestAddressAsString()
		{
		}
	}
}
