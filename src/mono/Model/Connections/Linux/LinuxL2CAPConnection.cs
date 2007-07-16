
using System;
using System.Runtime.InteropServices;
namespace monotooth.Model.Connections
{
	
	
	public class LinuxL2CAPConnection : L2CAPConnection
	{
		
		public LinuxL2CAPConnection()
		{
		}
		~LinuxL2CAPConnection()
		{
			close(this.sockf);
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
			this.toaddr = value;
			}
		}
		public void connect(int psm)
		{
			this.sockf = l2cap_connect(this.toaddr,(ushort)psm);
			this.isconnected = true;
		}
		public void connect()
		{
			this.sockf = l2cap_connect(this.toaddr,(ushort)11);
			this.isconnected = true;
		}
		public void disconnect()
		{
		}
		private bool isconnected = false;
		public bool isConnected()
		{
			return isconnected;
		}
		public void Write(System.Text.StringBuilder bytes)
		{
			bytes.Capacity = 1024;
			write(this.sockf,bytes,bytes.Capacity);
		}
		public void Read(System.Text.StringBuilder bytes)
		{
			bytes.Capacity = 1024;
			read(this.sockf,bytes,bytes.Capacity);
		}
		public void listen(int psm)
		{
			if(!isconnected || ( (psm % 2) != 0))
			{
			this.sockf = l2cap_listen((ushort)psm, 1);
			}
			else
			{
				 Console.WriteLine("You cannot listen to a port while connected and make sure that psm is odd.");
			}
		}
		public void listen(int psm, int maxconns)
		{
			if(!isconnected || ( (psm % 2) != 0))
			{
			this.sockf = l2cap_listen((ushort)psm, maxconns);
			}
			else
			{
				 Console.WriteLine("You cannot listen to a port while connected and make sure that psm is odd.");
			}
		}
		[DllImport("monotooth")]
		private static extern int l2cap_connect(monotooth.Model.BluetoothAddress to, ushort psm);
		[DllImport("monotooth")]
		private static extern int l2cap_listen(ushort psm, int maxconns);
		[DllImport("bluetooth")]
		private static extern int close(int sockf);		
		[DllImport("bluetooth")]
		private static extern int read(int sockf,System.Text.StringBuilder buf, int len);
		[DllImport("bluetooth")]
		private static extern int write(int sockf,System.Text.StringBuilder buf, int len);
	}
}
