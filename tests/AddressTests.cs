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
			monotooth.BluetoothAddress ba =	new monotooth.BluetoothAddress("80:7F:7E:7D:7C:7B");
			byte[] comp = new byte[]{ 128, 127, 126, 125, 124, 123};
			monotooth.BluetoothAddress ba2 = new monotooth.BluetoothAddress(comp);
			monotooth.BluetoothAddress zeroaddr = new monotooth.BluetoothAddress();			
			Assert.AreNotEqual(ba.Array, zeroaddr,"Should not return zero address with good bluetooth address!");
			Assert.AreEqual(ba2.Array,ba.Array, "Test data comparison failed!");
			monotooth.BluetoothAddress bogusba = new monotooth.BluetoothAddress("80LOL:FF:RFF:FFF:FFF:FFF:FF");
			Assert.AreEqual(zeroaddr.Array, bogusba.Array, "Bogus data accepted!");
			monotooth.BluetoothAddress toobigblocksba = new monotooth.BluetoothAddress("801322F:FF:FF:FFAF:FAF:5FF");
			Assert.AreEqual(zeroaddr.Array, toobigblocksba.Array,"Data with too big blocks accepted!");
		}
		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public void TestAddressAsString()
		{
			monotooth.BluetoothAddress batest = new monotooth.BluetoothAddress();
			batest.Array = new byte[]{128,127,126,125,124,123};
			string teststring = monotooth.BluetoothAddress.AddressAsString(batest);
			Assert.AreEqual("80:7F:7E:7D:7C:7B",teststring,"Good input address won't get in to a string!");
			batest.Array = new byte[]{1,10,13,14,15,11};			
			string zeroes = monotooth.BluetoothAddress.AddressAsString(batest);
			Assert.AreEqual("01:0A:0D:0E:0F:0B",zeroes,"Good input address isn't produced correctly!");
			batest.Array = new byte[]{1,2,4,2,1};
			string tooshort = monotooth.BluetoothAddress.AddressAsString(batest);
			Assert.AreEqual("00:00:00:00:00",tooshort,"Too short array got in to a string!");
		}
	}
}
