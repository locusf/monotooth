
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
			IntPtr lpwsaqueryset = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WSAQUERYSET)));
			WSAQUERYSET wsaqueryset = new WSAQUERYSET();
			wsaqueryset.dwSize = Marshal.SizeOf(typeof(WSAQUERYSET));
			wsaqueryset.dwNameSpace = 16;
			WSAData data = new WSAData();
			data.wHighVersion = 2;
			data.wVersion = 2;
			Marshal.StructureToPtr(wsaqueryset,lpwsaqueryset,false);
			int flags = (int)0x0002;
			flags |= (int)(0x1000|0x0010|0x0100);
			Int32 handle = 0;
			
			int result = 0;
			
			result = WSAStartup(36,data);
			if(result != 0)
			{
				throw new Exception("WSA Error, make sure you have the newest version (at least 2.2) of Winsock2!");
			}
			result = WSALookupServiceBegin(wsaqueryset,flags,ref handle);
			
			
			if (result == -1)
			{
				int error = WSAGetLastError();
				if (error == 10108) // No device attached
				{
					throw new Exception("You do not have a bluetooth device on your system.");					
				}
			}
			while (0 == result)
    		{
        		Int32 dwBuffer = 0x10000; 
        		IntPtr pBuffer = Marshal.AllocHGlobal(dwBuffer);
                
		        WSAQUERYSET qsResult = new WSAQUERYSET() ;
                    
        		result = WSALookupServiceNext(handle, flags, 
                ref dwBuffer, pBuffer);
        
        		if (0==result)
        		{
            		Marshal.PtrToStructure(pBuffer, qsResult);
            		CS_ADDR_INFO addr = new CS_ADDR_INFO();
            		Marshal.PtrToStructure(qsResult.lpcsaBuffer, addr);
            		sockaddr sa = new sockaddr();
            		Marshal.PtrToStructure(addr.RemoteAddr.lpSockaddr,sa);              		
            		monotooth.BluetoothAddress ba = new BluetoothAddress();
            		for (int i = 5; i >= 0; i--)
            		{
            			ba.b[i] = sa.sa_data[i];            			
            		}            		
            		WindowsRemoteDevice dev = new WindowsRemoteDevice(ba, qsResult.szServiceInstanceName);
            		pool.Add(dev);
            		
        		} else
        		{
        		WSALookupServiceEnd(handle);            	
        		}
        		Marshal.FreeHGlobal(pBuffer);
			}
			    		
			Marshal.FreeHGlobal(lpwsaqueryset);
			WSACleanup();
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
		public class sockaddr
		{
			public ushort sa_family;
			[MarshalAs(UnmanagedType.ByValArray,SizeConst=14)]
			public byte[] sa_data;
		}
		[StructLayout(LayoutKind.Sequential)]
		public class SOCKET_ADDRESS
		{
			public IntPtr lpSockaddr;
			public int iSockaddrLength;
		}
		[StructLayout(LayoutKind.Sequential)]
		public class CS_ADDR_INFO
		{
			public SOCKET_ADDRESS LocalAddr;
			public SOCKET_ADDRESS RemoteAddr;
			public int iSocketType;
			public int iProtocol;
		}
		[StructLayout(LayoutKind.Sequential)]
		public class SOCKADDR_BTH
		{
		public ushort addressFamily;
		public ulong btAddr;
		public ulong port;
		}
		[StructLayout(LayoutKind.Sequential)]
    	public class WSAData 
    	{
        public Int16 wVersion;
        public Int16 wHighVersion;  
        public String szDescription;  
        public String szSystemStatus;  
        public Int16 iMaxSockets;  
        public Int16 iMaxUdpDg;  
        public IntPtr lpVendorInfo;
    	}

    	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Auto )]
    	public class WSAQUERYSET
    	{
        public Int32 dwSize = 0;  
        public String szServiceInstanceName = null;  
        public IntPtr lpServiceClassId;  
        public IntPtr lpVersion;  
        public String lpszComment;  
        public Int32 dwNameSpace;  
        public IntPtr lpNSProviderId;  
        public String lpszContext;  
        public Int32 dwNumberOfProtocols;  
        public IntPtr lpafpProtocols;  
        public String lpszQueryString;  
        public Int32 dwNumberOfCsAddrs;  
        public IntPtr lpcsaBuffer;  
        public Int32 dwOutputFlags;  
        public IntPtr lpBlob;
    	} 
    
    	[DllImport("Ws2_32.DLL", CharSet = CharSet.Auto, 
    	SetLastError=true)]
    	private extern static
        Int32 WSAStartup( Int16 wVersionRequested, 
            WSAData wsaData );

    	[DllImport("Ws2_32.DLL", CharSet = CharSet.Auto, 
    	SetLastError=true)]
    	private extern static	
        Int32 WSACleanup();

    	[DllImport("Ws2_32.dll", CharSet = CharSet.Auto, 
    	SetLastError=true)]
    	private extern static
        Int32 WSALookupServiceBegin(WSAQUERYSET qsRestrictions,
            Int32 dwControlFlags, ref Int32 lphLookup);


    	[DllImport("Ws2_32.dll", CharSet = CharSet.Auto, 
    	SetLastError=true)]
    	private extern static
        Int32 WSALookupServiceNext(Int32 hLookup,
            Int32 dwControlFlags,
            ref Int32 lpdwBufferLength,
            IntPtr pqsResults);

    	[DllImport("Ws2_32.dll", CharSet = CharSet.Auto, 
    	SetLastError=true)]
    	private extern static
        Int32 WSALookupServiceEnd(Int32 hLookup);
    	
    	[DllImport("Ws2_32.dll", CharSet = CharSet.Auto, 
    	SetLastError=true)]
    	private extern static
    		Int32 WSAGetLastError();
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
	}
}
