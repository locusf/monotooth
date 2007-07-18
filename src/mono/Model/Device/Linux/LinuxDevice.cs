
using System.Runtime.InteropServices;
using System;

namespace monotooth.Model.Device
{
	
	/// <summary> The linux implementation of <c>IDevice</c></summary>	
	public class LinuxDevice : IDevice
	{
		/// <summary> The default constructor for linux implementation </summary>
		public LinuxDevice()
		{
			this.dev_id = hci_get_route(IntPtr.Zero);
			if(this.dev_id != -1)
			{
			int dd = hci_open_dev(this.dev_id);
			this.address = new monotooth.Model.BluetoothAddress();
			IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(monotooth.Model.BluetoothAddress)));
			hci_devba(this.dev_id,ptr);
			Marshal.PtrToStructure(ptr,this.address);		
			System.Text.StringBuilder bld = new System.Text.StringBuilder(248);
			hci_read_local_name(dd,bld.Capacity,bld,1000);
			this.name = bld.ToString();
			Marshal.FreeHGlobal(ptr);
			} else
			{
				Console.WriteLine("You do not have a bluetooth device in your system.");
			}
		}
		/// <summary> This is the alternative constructor for constructing a
		/// LinuxDevice with specific address and name.</summary>
		public LinuxDevice(monotooth.Model.BluetoothAddress addr, string name)
		{
			this.address = addr;
			this.name = name;
		}
		// Instance variables
		private int dev_id = -1;
		private monotooth.Model.BluetoothAddress address;
		private string name;
		private monotooth.Model.Service.ServicePool services;
		/// <summary> Implemented properties from IDevice, this property defines address. </summary>
		public monotooth.Model.BluetoothAddress Address 
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
		/// <summary> Implemented properties from IDevice, this property defines a pool of Services. </summary>
		public monotooth.Model.Service.ServicePool Services
		{
			get { return this.services; }
			set { this.services = value; }
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
			int count=0;			
			InquiryInformation info = new InquiryInformation();		
			IntPtr devs = inquire_devices();
			IntPtr element;
			if(!(devs==IntPtr.Zero))
			{
			while (Marshal.ReadIntPtr(devs,count*IntPtr.Size) != IntPtr.Zero)
			{
				++count;
			}
						
			if (count>0)
			{
				for(int i = 0; i < count;i++)
				{
					try
					{
					element = Marshal.ReadIntPtr(devs,i*IntPtr.Size);
					info = (InquiryInformation)Marshal.PtrToStructure(element,typeof(InquiryInformation));					
					System.Text.StringBuilder bld2 = new System.Text.StringBuilder(248);					
					if(hci_read_remote_name(sock,info.bdaddr,248,bld2,0)==0)
					{					
						LinuxDevice dev = new LinuxDevice(info.bdaddr,bld2.ToString());
						pool.Add(dev);						
					}
					// this catch is meant to skip invalid pointers
					} catch(NullReferenceException nre)
					{
						
						nre.Equals(nre);
					}
					//Marshal.FreeHGlobal(element);
				}
			}
			}			
			Marshal.FreeHGlobal(devs);			
			close(sock);
			return pool;
			} else 
			{
			Console.WriteLine("You do not have a bluetooth device in your system.");
			return null;
			}
		}
		/// <summary> Inquires services from a given device. The service UUID can also be specified. </summary>
		/// <param name="dev">An instance of an <c>IDevice</c>, used to dig up an address. </param>
		/// <param name="uuid">An unsigned integer to describe a certain service. </param>
		public monotooth.Model.Service.ServicePool InquireServices(IDevice dev,uint uuid)
		{			
			IntPtr element,services;
			if(uuid == 0)
			{
			services = search_services_from(dev.Address);
			} else
			{
			services = search_services_from_with_uuid(dev.Address,uuid);
			}
			monotooth.Model.Service.ServicePool pool = new monotooth.Model.Service.ServicePool();
			Service serv = new Service();			
			int count = 0;
			while(Marshal.ReadIntPtr(services,count*IntPtr.Size) != IntPtr.Zero)
			{
				++count;
			}			
			if(!(services==IntPtr.Zero))
			{									
			if (count>0)
			{
				for(int i = 0; i < count;i++)
				{
					try
					{
					element = Marshal.ReadIntPtr(services,i*IntPtr.Size);
					if(element != IntPtr.Zero)
					{
					serv = (Service) Marshal.PtrToStructure(element,typeof(Service));
					if(serv.rfcomm_port<32 && serv.rfcomm_port != 0)
					{
					pool.Add(serv);
					}
					}
					// this catch is meant to skip invalid pointers
					} catch (NullReferenceException nre)
					{		
						nre.Equals(nre);
					}
				}
			}
			}
			Marshal.FreeHGlobal(services);
			return pool;
		}
		/// <summary>Returns the devices address as string. </summary>
		/// <returns>A string with the native address of the device. </returns>
		public string AddressAsString()
		{
			string ret = Marshal.PtrToStringAnsi(batostr(this.address));
			return ret;
		}
		/// <summary>Returns an address as string. </summary>
		/// <returns>A string from the address.</returns>
		public static string AddressAsString(monotooth.Model.BluetoothAddress ba)
		{			
			string ret = Marshal.PtrToStringAnsi(batostr(ba));
			return ret;
		}
		/// <summary>Returns an address as string. </summary>
		/// <returns>A string from the address.</returns>
		/// <param name="addr"> string presentation of the address. </param>
		public static monotooth.Model.BluetoothAddress StringAsAddressStatic(string addr)
		{
			
			if(addr.LastIndexOfAny(new char[1]{':'})==14)
			{
			monotooth.Model.BluetoothAddress ba = new monotooth.Model.BluetoothAddress();
			Marshal.PtrToStructure(strtoba(addr),ba);
			return ba;
			} else {
			return new monotooth.Model.BluetoothAddress();
			}
		}
		/// <summary>Returns a <c>monotooth.Model.BluetoothAddress</c> from a string, using a native function. </summary>
		/// <returns>New address. </returns>
		/// <remarks>Will return an 0-address if the address string is not in the 48-bit form. </remarks>
		public monotooth.Model.BluetoothAddress StringAsAddress(string addr)
		{
			if(addr.LastIndexOfAny(new char[1]{':'})==15)
			{
			monotooth.Model.BluetoothAddress ba = new monotooth.Model.BluetoothAddress();
			Marshal.PtrToStructure(strtoba(addr),ba);
			return ba;
			} else {
			return new monotooth.Model.BluetoothAddress();
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
		private static extern int hci_read_remote_name(int dd, monotooth.Model.BluetoothAddress bdaddr, int len, System.Text.StringBuilder b, int timeout);
		[DllImport("bluetooth")]
		private static extern int hci_read_local_name(int dd, int len, System.Text.StringBuilder b,int timeout);
		[DllImport("monotooth")]
		// Search for devices with this function
		private static extern IntPtr inquire_devices();
		[DllImport("monotooth")]
		private static extern IntPtr search_services_from(monotooth.Model.BluetoothAddress ba);
		[DllImport("monotooth")]
		private static extern IntPtr search_services_from_with_uuid(monotooth.Model.BluetoothAddress ba, uint uuid);
		/*
			Utility functions
		*/
		[DllImport("bluetooth")]
		private static extern IntPtr batostr(monotooth.Model.BluetoothAddress ba);
		[DllImport("bluetooth")]
		private static extern IntPtr strtoba(string addr);
		/*
		
		Native library helper classes
		
		*/
		/// <summary>A structure to describe a service.</summary>
		[StructLayout (LayoutKind.Sequential, Pack = 1)]
		public struct Service
		{
		/// <summary>A port of the service. </summary>
		public int rfcomm_port;
		/// <summary>Name of the service. </summary>
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=256)]
		public char[] name;
		/// <summary>Description of the service.</summary>
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=256)]
		public char[] description;
		}
		[StructLayout (LayoutKind.Sequential)]
		private class InquiryInformation
		{
			public InquiryInformation()
			{}
			public monotooth.Model.BluetoothAddress bdaddr;
			public byte pscan_rep_mode;
			public byte pscan_period_mode;
			public byte pscan_mode;
			[MarshalAs(UnmanagedType.ByValArray,SizeConst=3)]
			public byte[] dev_class;
			public ushort clock_offset;
		}
		
	}
}
