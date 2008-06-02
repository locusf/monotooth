using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Globalization;
namespace monotooth
{	
		/// <summary>A class that describes a bluetooth address. This class is compatible with bluez structure
		/// bdaddr_t. </summary>
		[Serializable()]
		[StructLayout (LayoutKind.Sequential)]
		public class BluetoothAddress
		{
		/// <summary>The default constructor. Initializes the address as BDADDR_ANY. </summary>
		public BluetoothAddress()
		{
			this.b = new byte[6]{ 0, 0, 0, 0, 0, 0};
		}
		/// <summary>The array of unsigned integers to hold the 48-bit address. </summary>
		[MarshalAs (UnmanagedType.ByValArray, SizeConst=6)]
		public byte[] b;		
		/// <summary>Returns a <c>monotooth.BluetoothAddress</c> from a string. </summary>
		/// <returns>New address. </returns>
		/// <remarks>Will return a 0-address, if the address string is not in the 48-bit form. </remarks>
		public static monotooth.BluetoothAddress StringAsAddress(string addr)
		{			
			string[] splits = addr.Split(new char[]{':'});
			int i = 0;
			monotooth.BluetoothAddress ba = new BluetoothAddress();			
			if (splits.Length == 6)
			{
				foreach(string split in splits)
				{
					int block = (int)Int32.Parse(split,NumberStyles.HexNumber);
					if (block >= 0 && block <= 255)
					{
					ba.b[i] = (byte)block;
					i++;
					} else
					{
						return new monotooth.BluetoothAddress();
					}
				}
				return ba;
			} else {
			return new monotooth.BluetoothAddress();
			}
		}		
		/// <summary>Returns an address as string. </summary>
		/// <returns>A string from the address.</returns>
		public static string AddressAsString(monotooth.BluetoothAddress ba)
		{		
			if(ba == null) throw new ArgumentException("ba","May not be null!");
			if(ba.b.Length == 0) throw new ArgumentException("ba","May not be empty!");
			if(ba.b.Length != 6) throw new ArgumentException("ba","Too short for address format!");
			string ret = "";
			foreach (byte b in ba.b)
			{
				if (b < 16)
				{
					ret+="0"+Convert.ToString(b,16)+":";
				} else {					
					ret+=Convert.ToString(b,16)+":";
				}
			}
			ret = ret.Remove(ret.Length-1,1);
			ret = ret.ToUpper();
			return ret;
		}
		}
}
