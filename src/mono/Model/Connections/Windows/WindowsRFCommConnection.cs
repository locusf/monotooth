
using System;

namespace monotooth.Model.Connections
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
		public void ReadByte(System.Text.StringBuilder onebyte)
		{
		}
		public void WriteByte(System.Text.StringBuilder onebyte)
		{
		}
		public void listen(int channel)
		{
		}
		public void listen(int channel, int maxconns)
		{
		}
		private int sockf;
		public int SocketDescriptor
		{
			get
			{
				return sockf;
			}
		}
		private monotooth.Model.BluetoothAddress fromaddr;
		public monotooth.Model.BluetoothAddress from
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
		private monotooth.Model.BluetoothAddress toaddr;
		public monotooth.Model.BluetoothAddress to
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
		private int usedbytes;
		public int BytesUsed
		{
			get
			{
				return usedbytes;
			}
		}
	}
}
