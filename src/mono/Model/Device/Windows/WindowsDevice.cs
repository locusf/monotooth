
using System;
using System.Runtime.InteropServices;
namespace monotooth.Device
{
	
	
	public class WindowsDevice : ILocalDevice
	{
		
		public WindowsDevice()
		{
			
		}
		// Instance variables
		private monotooth.BluetoothAddress address;
		private string name;
		private monotooth.Connections.IConnection conn;
		private monotooth.Service.ServicePool services;
		// Implemented properties from IDevice
		public monotooth.BluetoothAddress Address 
		{
			get { return this.address; }
			set { this.address = value; }
		}
		public string FriendlyName
		{
			get { return this.name; }
			set { this.name = value; }
		}
		public monotooth.Connections.IConnection Connection
		{
			get { return this.conn; }
			set { this.conn = value;}
		}
		public monotooth.Service.ServicePool Services
		{
			get { return this.services; }
			set { this.services = value; }
		}
		// Implemented functions from IDevice
		public DevicePool Inquire()
		{
			DevicePool pool = new DevicePool();
			
			return pool;
		}
		public string AddressAsString()
		{
			return "";
		}
		public monotooth.BluetoothAddress StringAsAddress(string addr)
		{
			return null;
		}
		[StructLayout(LayoutKind.Sequential)]
		private struct BTHNS_INQUIRYBLOB
		{
			public UInt32 LAP;
			public Byte length;
		}
		[StructLayout(LayoutKind.Sequential)]
		private struct WSAQUERYSET
		{
			public UInt32 dwSize;
			public string lpszServiceInstanceName;
			public GUID lpServiceClassId;
			
		}
		[StructLayout(LayoutKind.Sequential)]
		private struct GUID
		{
			public UInt32 Data1;
			public UInt16 Data2;			
			public UInt16 Data3;
			[MarshalAs(UnmanagedType.ByValArray,SizeConst=8)]
			public byte[] Data4;
		}
		[StructLayout(LayoutKind.Sequential)]
		private struct BLOB
		{
			public UInt32 cbSize;
			public IntPtr pBlobData;
		}
		/*[StructLayout(LayoutKind.Sequential)]
		private struct BLUETOOTH_DEVICE_SEARCH_PARAMS
		{
			public System.UInt32 dwSize;
			public bool fReturnAuthenticated;
			public bool fReturnRemembered;
			public bool fReturnUnknown;
			public bool fReturnConnected;
			public bool fIssueInquiry;
			public short cTimeoutMultiplier;
			public IntPtr hRadio;
		}
		[StructLayout(LayoutKind.Sequential)]
		private struct BLUETOOTH_DEVICE_INFO
		{
			public System.UInt32 dwSize;
			public BLUETOOTH_ADDRESS_BYTES Address;
			public System.UInt32 ulClassofDevice;
			public bool fConnected;
			public bool fRemembered;
			public bool fAuthenticated;
			public SYSTEMTIME stLastSeen;
			public SYSTEMTIME stLastUsed;
			[MarshalAs(UnmanagedType.ByValArray,SizeConst=248)]
			public char[] szName;
		}
		[StructLayout(LayoutKind.Explicit)]
		private struct BLUETOOTH_ADDRESS_LONG
		{
			[FieldOffset(0)]
			public long ullLong;			
		}
		[StructLayout(LayoutKind.Explicit)]
		private struct BLUETOOTH_ADDRESS_BYTES
		{
			[FieldOffset(0),MarshalAs(UnmanagedType.ByValArray,SizeConst=6)]
			public byte[] rgBytes;		
		}
		[StructLayout(LayoutKind.Sequential)]
		private struct BLUETOOTH_FIND_RADIO_PARAMS
		{
			public uint dwSize;
		}
		[StructLayout(LayoutKind.Sequential)]
		private struct BLUETOOTH_RADIO_INFO
		{
			public uint dwSize;
			public BLUETOOTH_ADDRESS_BYTES address;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=248)]
			public char[] szName;
			public uint ulClassOfDevice;
			public UInt16 ImpSubversion;
			public UInt16 manufacturer;
			
		}
		[StructLayout(LayoutKind.Sequential)]
		private struct SYSTEMTIME
		{
			public UInt16 wYear;
			public UInt16 wMonth;
			public UInt16 wDayOfWeek;
			public UInt16 wDay;
			public UInt16 wHour;
			public UInt16 wMinute;
			public UInt16 wSecond;
			public UInt16 wMilliseconds;
		}
		// Imported native functions
		[DllImport("irprops.cpl")]
		private static extern IntPtr BluetoothFindFirstDevice(IntPtr btsp, IntPtr btdi);
		[DllImport("irprops.cpl")]
		private static extern bool BluetoothFindNextDevice(IntPtr hfind, BLUETOOTH_DEVICE_INFO pbtdi);
		[DllImport("irprops.cpl")]
		private static extern void BluetoothFindDeviceClose(IntPtr hfind);
		[DllImport("irprops.cpl")]
		private static extern IntPtr BluetoothFindFirstRadio(IntPtr parms, IntPtr handle);
		[DllImport("irprops.cpl")]
		private static extern uint BluetoothGetRadioInfo(IntPtr hRadio, IntPtr pRadioInfo);
		[DllImport("kernel32")]
		private static extern System.UInt32 GetLastError();
		[DllImport("kernel32")]
		private static extern System.UInt32 FormatMessage(UInt32 dwFlags, IntPtr source, UInt32 dwMessageId, UInt32 dwLanguageId, System.Text.StringBuilder lpbuffer, UInt32 nSize, IntPtr Arguments);*/
		
	}
}
