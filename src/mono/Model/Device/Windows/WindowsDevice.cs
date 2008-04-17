
using System;
using System.Runtime.InteropServices;
namespace monotooth.Device
{
	
	
	public class WindowsDevice : ILocalDevice
	{
		
		public WindowsDevice()
		{
			BLUETOOTH_FIND_RADIO_PARAMS parms = new BLUETOOTH_FIND_RADIO_PARAMS();
			parms.dwSize = (uint)Marshal.SizeOf(typeof(BLUETOOTH_FIND_RADIO_PARAMS));
			IntPtr handle = Marshal.AllocHGlobal(Marshal.SizeOf(IntPtr.Size));
			IntPtr pparms = Marshal.AllocHGlobal(Marshal.SizeOf(parms.dwSize));
			Marshal.StructureToPtr(parms,pparms,true);
			BluetoothFindFirstRadio( pparms,handle);
			if(handle != IntPtr.Zero)
			{
				BLUETOOTH_RADIO_INFO rInfo = new BLUETOOTH_RADIO_INFO();
				rInfo.dwSize = (uint) Marshal.SizeOf(typeof(BLUETOOTH_RADIO_INFO));
				IntPtr pRadioInfo = Marshal.AllocHGlobal((int)rInfo.dwSize);
				Marshal.StructureToPtr(rInfo,pRadioInfo,true);
				uint ret = BluetoothGetRadioInfo(handle,pRadioInfo);
				UInt32 err = GetLastError();
				Console.WriteLine("ERROR: "+err);
				System.Text.StringBuilder bld = new System.Text.StringBuilder(512);			
				UInt32 bytes = FormatMessage((UInt32)0x00001000, IntPtr.Zero, err, 0,bld,511,IntPtr.Zero);
				Console.WriteLine("ERROR as String: "+bld.ToString());
				if(ret == 0)
				{
					Console.WriteLine(new string(rInfo.szName));
				}
			}
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
			BLUETOOTH_DEVICE_SEARCH_PARAMS bdsp = new BLUETOOTH_DEVICE_SEARCH_PARAMS();
			IntPtr pbdsp,pbdi;
			pbdsp = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BLUETOOTH_DEVICE_SEARCH_PARAMS)));
			pbdi = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BLUETOOTH_DEVICE_INFO)));
			bdsp.dwSize = (uint)Marshal.SizeOf(typeof(BLUETOOTH_DEVICE_SEARCH_PARAMS));
			bdsp.fReturnAuthenticated = true;
			bdsp.fReturnConnected = true;
			bdsp.fReturnRemembered = true;
			bdsp.fReturnUnknown = true;
			bdsp.fIssueInquiry = true;
			bdsp.cTimeoutMultiplier = 10;
			bdsp.hRadio = Marshal.AllocHGlobal(0);
			BLUETOOTH_DEVICE_INFO bdi = new BLUETOOTH_DEVICE_INFO();
			bdi.dwSize = (uint)Marshal.SizeOf(typeof(BLUETOOTH_DEVICE_INFO));
			Marshal.StructureToPtr(bdsp,pbdsp,true);
			Marshal.StructureToPtr(bdi,pbdi,true);
			IntPtr hbf = Marshal.AllocHGlobal(IntPtr.Size);
			hbf = BluetoothFindFirstDevice(pbdsp,pbdi);
			UInt32 err = GetLastError();
			Console.WriteLine("ERROR: "+err);
			System.Text.StringBuilder bld = new System.Text.StringBuilder(512);			
			UInt32 bytes = FormatMessage((UInt32)0x00001000, IntPtr.Zero, err, 0,bld,511,IntPtr.Zero);
			Console.WriteLine("ERROR as String: "+bld.ToString());
			if (hbf != IntPtr.Zero)
			{
				while(true)
				{
					BLUETOOTH_DEVICE_INFO binfo = (BLUETOOTH_DEVICE_INFO)Marshal.PtrToStructure(pbdi,typeof(BLUETOOTH_DEVICE_INFO));
					Console.WriteLine(new string(binfo.szName));
					if(BluetoothFindNextDevice(hbf,bdi)==false) break;
				}
				BluetoothFindDeviceClose(hbf);
			}
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
		private static extern System.UInt32 FormatMessage(UInt32 dwFlags, IntPtr source, UInt32 dwMessageId, UInt32 dwLanguageId, System.Text.StringBuilder lpbuffer, UInt32 nSize, IntPtr Arguments);
	}
}
