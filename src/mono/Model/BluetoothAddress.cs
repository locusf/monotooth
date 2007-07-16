using System;
using System.Runtime.InteropServices;
using System.Text;
namespace monotooth.Model
{	
		/// <summary>A class that describes a bluetooth address. This class is compatible with bluez structure
		/// bdaddr_t. </summary>
		[StructLayout (LayoutKind.Sequential)]
		public class BluetoothAddress
		{
		/// <summary>The default constructor. Initializes the address as BDADDR_ANY. </summary>
		public BluetoothAddress()
		{
			this.b = new uint[6]{ 0, 0, 0, 0, 0, 0};
		}
		/// <summary>The array of unsigned integers to hold the 48-bit address. </summary>
		[MarshalAs (UnmanagedType.ByValArray, SizeConst=6)]
		public uint[] b;		
		}
}
