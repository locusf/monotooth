
using System;
using System.Runtime.InteropServices;
namespace monotooth.Model.Connections
{
	
	
	public class LinuxRFCommConnection : RFCommConnection
	{
		
		public LinuxRFCommConnection()
		{
		}
		~LinuxRFCommConnection()
		{
			close(this.sockf);
		}
		public void connect()
		{						
			this.sockf = rfcomm_connect(this.fromaddr,this.toaddr,0);
			this.chan = 0;
			this.connected = true;
		}
		public void connect(int channel)
		{
			this.sockf = rfcomm_connect(this.fromaddr,this.toaddr,channel);
			this.chan = channel;
			this.connected = true;
		}
		public void disconnect()
		{
			close(this.sockf);
			this.connected = false;
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
			return this.connected;
		}
		public void Write(System.Text.StringBuilder bytes)
		{
			if(this.connected)
			{
			bytes.Capacity = 1024;
			write(this.sockf,bytes,bytes.Capacity);
			}
		}
		public void Read(System.Text.StringBuilder bytes)
		{
			if(this.connected)
			{
			bytes.Capacity = 1024;
			read(this.sockf,bytes,bytes.Capacity);
			}
		}
		public void listen(int channel)
		{
			this.chan = channel;
			if(!isConnected())
			{
				this.sockf = rfcomm_listen(this.fromaddr,channel,1);
				this.Connected = true;
			} else
			{
				Console.WriteLine("Can't listen to an already open connection!");
			}
	
		}
		public void listen(int channel, int maxconns)
		{
			this.chan = channel;
			if(!isConnected())
			{
				this.sockf = rfcomm_listen(this.fromaddr,channel,maxconns);				
			} else
			{
				Console.WriteLine("Can't listen to an alReady open connection!");
			}
		}
		/*
			Native bluetooth library calls
		*/
		
		[DllImport("monotooth")]
		private static extern int rfcomm_connect(monotooth.Model.BluetoothAddress from, monotooth.Model.BluetoothAddress to, int channel);
		[DllImport("monotooth")]
		private static extern int rfcomm_listen(monotooth.Model.BluetoothAddress from, int channel, int backlog);		
		[DllImport("bluetooth")]
		private static extern int close(int sockf);		
		[DllImport("bluetooth")]
		private static extern int read(int sockf,System.Text.StringBuilder buf, int len);
		[DllImport("bluetooth")]
		private static extern int write(int sockf,System.Text.StringBuilder buf, int len);
		/*
			Helper structures
		*/
		
		/*
			Interface Properties 
		*/
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
	}
}
