
using System.Runtime.InteropServices;
using System;

namespace monotooth.Device
{
	
	/// <summary> The linux implementation of <c>IDevice</c></summary>	
	public class LinuxDevice : ILocalDevice
	{
		/// <summary> The default constructor for linux implementation </summary>
		public LinuxDevice()
		{
			this.dev_id = hci_get_route(IntPtr.Zero);
			if(this.dev_id != -1)
			{
			int dd = hci_open_dev(this.dev_id);
			this.address = new monotooth.BluetoothAddress();
			IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(monotooth.BluetoothAddress)));
			hci_devba(this.dev_id,ptr);
			Marshal.PtrToStructure(ptr,this.address);		
			System.Text.StringBuilder bld = new System.Text.StringBuilder(248);
			hci_read_local_name(dd,bld.Capacity,bld,1000);
			this.name = bld.ToString();
			Marshal.FreeHGlobal(ptr);
			} else
			{
				throw new Exception("You do not have a bluetooth device on your system.");
			}
		}
		
		// Instance variables
		private int dev_id = -1;
		private monotooth.BluetoothAddress address;
		private string name;
		public int Device_Indentifier
		{
			get
			{
				return dev_id;
			}
		}
		/// <summary> Implemented properties from IDevice, this property defines address. </summary>
		public monotooth.BluetoothAddress Address 
		{
			get { return this.address; }
			set { this.address = value; }
		}
		/// <summary> Implemented properties from IDevice, this property defines FriendlyName of the device. </summary>
		public string FriendlyName
		{
			get { return this.name; }
			set { this.name = value; }
		}					
		/// <summary> Inquire devices from the surrounding area, uses a native method to achieve good inquiry. </summary>
		/// <returns> A <c>DevicePool</c>, in which all the devices are added, if any. </returns> 
		public DevicePool Inquire()
		{			
			this.dev_id = hci_get_route(IntPtr.Zero);
			if( this.dev_id != -1)
			{
			int sock = hci_open_dev(this.dev_id);
			DevicePool pool = new DevicePool();
			//int count=0;			
			inquiry_info info = new inquiry_info();		
			IntPtr devs = IntPtr.Zero;
			
			byte[] lap = new byte[] { 0x33, 0x8b, 0x9e};
			int max_rsp = 255;
			int length = 8;
			int flags = 1;
			
			int count = hci_inquiry(this.dev_id, length, max_rsp, lap, out devs,flags);
			if(!(devs==IntPtr.Zero))
			{
			/*while (Marshal.ReadIntPtr(devs,count*IntPtr.Size) != IntPtr.Zero)
			{
				++count;
			}*/
			IntPtr element = devs;
			if (count>0)
			{
				for(int i = 0; i < count;i++)
				{
					//try
					//{
					
					info = (inquiry_info)Marshal.PtrToStructure(element,typeof(inquiry_info));
					element = (IntPtr)((int)element+Marshal.SizeOf(typeof(inquiry_info)));
					System.Text.StringBuilder bld2 = new System.Text.StringBuilder(248);	
					monotooth.BluetoothAddress ba = new BluetoothAddress();
					ba.b = info.bdaddr;
					if(hci_read_remote_name(sock,ba,bld2.Capacity,bld2,0)==0)
					{					
						
						LinuxRemoteDevice dev = new LinuxRemoteDevice(ba,bld2.ToString());
						pool.Add(dev);						
					} else {
						LinuxRemoteDevice dev = new LinuxRemoteDevice(ba,"NONAME");
						pool.Add(dev);
					}
					// this catch is meant to skip invalid pointers
					//} catch(NullReferenceException nre)
					//{
						
						//nre.Equals(nre);
					//}
					//Marshal.FreeHGlobal(element);
				}
			}
			}			
			//Marshal.FreeHGlobal(devs);			
			close(sock);
			return pool;
			} else 
			{
			throw new Exception("You do not have a bluetooth device on your system.");
			}
		}
		
		/// <summary>Returns the devices address as string. </summary>
		/// <returns>A string with the native address of the device. </returns>
		public string AddressAsString()
		{
			System.Text.StringBuilder bld = new System.Text.StringBuilder(18);			
			ba2str(this.address,bld);			
			return bld.ToString();
		}
		/// <summary>Returns an address as string. </summary>
		/// <returns>A string from the address.</returns>
		public static string AddressAsString(monotooth.BluetoothAddress ba)
		{		
			if(ba == null) throw new ArgumentException("ba","May not be null!");
			System.Text.StringBuilder bld = new System.Text.StringBuilder(18);
			monotooth.BluetoothAddress b = new monotooth.BluetoothAddress();
			baswap(b,ba);
			ba2str(b,bld);			
			return bld.ToString();
		}
		/// <summary>Returns an address as string. </summary>
		/// <returns>A string from the address.</returns>
		/// <param name="addr"> string presentation of the address. </param>
		public static monotooth.BluetoothAddress StringAsAddressStatic(string addr)
		{
			
			if(addr.LastIndexOfAny(new char[1]{':'})==15 && addr.Length == 17)
			{
			monotooth.BluetoothAddress ba = new monotooth.BluetoothAddress();
			Marshal.PtrToStructure(strtoba(addr),ba);
			return ba;
			} else {
			return new monotooth.BluetoothAddress();
			}
		}
		/// <summary>Returns a <c>monotooth.BluetoothAddress</c> from a string, using a native function. </summary>
		/// <returns>New address. </returns>
		/// <remarks>Will return a 0-address, if the address string is not in the 48-bit form. </remarks>
		public monotooth.BluetoothAddress StringAsAddress(string addr)
		{
			if(addr.LastIndexOfAny(new char[1]{':'})==15 && addr.Length == 17)
			{
			monotooth.BluetoothAddress ba = new monotooth.BluetoothAddress();
			Marshal.PtrToStructure(strtoba(addr),ba);
			return ba;
			} else {
			return new monotooth.BluetoothAddress();
			}
		}		
		
		/*		
		
		Native library calls from libbluetooth
		
		*/
		
		[DllImport("bluetooth")]
		// Use this call to get the local device id (dev_id), p = IntPtr.Zero
		private static extern int hci_get_route(IntPtr p);		
		[DllImport("bluetooth")]
		private static extern int hci_devba(int dev_id,IntPtr ba);
		[DllImport("bluetooth")]
		private static extern int hci_open_dev(int dev_id);
		[DllImport("bluetooth")]
		private static extern int close(int sockf);
		[DllImport("bluetooth")]
		private static extern int hci_read_remote_name(int dd, monotooth.BluetoothAddress ba, int len, System.Text.StringBuilder b, int timeout);
		[DllImport("bluetooth")]
		private static extern int hci_read_local_name(int dd, int len, System.Text.StringBuilder b,int timeout);
		[DllImport("bluetooth")]
		private static extern int hci_inquiry(int dev_id, int len, int num_rsp, byte[] lap,out IntPtr ii, int flags);
		[DllImport("monotooth")]
		// Search for devices with this function
		private static extern IntPtr inquire_devices();		
		
		/*
			Utility functions
		*/
		[DllImport("bluetooth")]
		private static extern int ba2str(monotooth.BluetoothAddress ba, System.Text.StringBuilder bld);
		[DllImport("bluetooth")]
		private static extern IntPtr strtoba(string addr);
		[DllImport("bluetooth")]
		private static extern void baswap(monotooth.BluetoothAddress ba, monotooth.BluetoothAddress ba2);
		/*
		
		Native library helper classes
		
		*/
		[StructLayout (LayoutKind.Sequential)]
		private class inquiry_info
		{
			public inquiry_info()
			{}
			[MarshalAs(UnmanagedType.ByValArray,SizeConst=6)]
			public byte[] bdaddr;
			public byte pscan_rep_mode;
			public byte pscan_period_mode;
			public byte pscan_mode;
			[MarshalAs(UnmanagedType.ByValArray,SizeConst=3)]
			public byte[] dev_class;
			public ushort clock_offset;
		}
		[StructLayout (LayoutKind.Sequential)]
		private class InquiryInformation
		{
			public InquiryInformation()
			{}
			public monotooth.BluetoothAddress bdaddr;
			public byte pscan_rep_mode;
			public byte pscan_period_mode;
			public byte pscan_mode;
			[MarshalAs(UnmanagedType.ByValArray,SizeConst=3)]
			public byte[] dev_class;
			public ushort clock_offset;
		}
		
	}
}
