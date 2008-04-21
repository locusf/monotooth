using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
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
		}
}
