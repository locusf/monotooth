
using System;
using System.Runtime.InteropServices;

namespace monotooth.Connections
{
	
	
	public class WindowsRFCommConnection : RFCommConnection
	{
		
		public WindowsRFCommConnection()
		{
		}
		public void connect()
		{
		}
		public void connect(int channel)
		{
		}
		public void disconnect()
		{
		}
		private bool connected = false;
		public bool Connected
		{
			get
			{
				return this.connected;
			}
			set
			{
				this.connected = value;
			}
		}
		public bool isConnected()
		{
			return false;
		}
		public void Write(System.Text.StringBuilder bytes)
		{
		}
		public void Read(System.Text.StringBuilder bytes)
		{
		}
		public void ReadWithOffset(IntPtr onebyte, int offset, int count)
		{
		}
		public void WriteWithOffset(IntPtr onebyte, int offset, int count)
		{
		}
		public void listen(int channel)
		{
		}
		public void listen(int channel, int maxconns)
		{
		}
		private int sockf = 0;
		public int SocketDescriptor
		{
			get
			{
				return sockf;
			}
		}
		private monotooth.BluetoothAddress fromaddr;
		public monotooth.BluetoothAddress from
		{
			get
			{
			return fromaddr;
			}			
			set
			{
			this.fromaddr = value;
			}
		}
		private monotooth.BluetoothAddress toaddr;
		public monotooth.BluetoothAddress to
		{
			get
			{
			return toaddr;
			}
			set
			{
			toaddr = value;
			}
		}
		private int chan;
		public int Channel
		{
			get
			{
				return chan;
			}
			set
			{
				this.chan = value;
			}
		}
		private int usedbytes = 0;
		public int BytesUsed
		{
			get
			{
				return usedbytes;
			}
		}
		[StructLayout(LayoutKind.Sequential)]
		private class SockAddr
		{
			public ushort sa_family;
			[MarshalAs(UnmanagedType.ByValArray,SizeConst=14)]
			public byte[] sa_data;
		}
		[DllImport("Ws2_32.dll")]
		private static extern int socket(int af, int type, int protocol);
		[DllImport("Ws2_32.dll")]
		private static extern int bind(int sockf, SockAddr addr, int len);
		[DllImport("Ws2_32.dll",EntryPoint="connect")]
		private static extern int connecti(int sockf, SockAddr addr, int len);
		[DllImport("Ws2_32.dll")]
		private static extern int accept();
	}
}
