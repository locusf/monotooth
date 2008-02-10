
using System;
using System.Runtime.InteropServices;
namespace monotooth.Device
{
	
	
	public class LinuxRemoteDevice : IRemoteDevice
	{
		
		private LinuxRemoteDevice()
		{
		}
		public LinuxRemoteDevice(monotooth.BluetoothAddress ba, string name)
		{
			this.address = ba;
			this.name = name;
		}
		// Instance variables
		private monotooth.BluetoothAddress address;
		private string name;
		private monotooth.Service.ServicePool services;
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
		/// <summary> Implemented properties from IDevice, this property defines a pool of Services. </summary>
		public monotooth.Service.ServicePool Services
		{
			get { return this.services; }
			set { this.services = value; }
		}
		/// <summary> Inquires services from a this device. The service UUID can also be specified. </summary>
		/// <param name="uuid">An unsigned integer to describe a certain service. </param>
		public monotooth.Service.ServicePool InquireServices(uint uuid)
		{			
			IntPtr element,services;			
			services = search_services_from_with_uuid(this.Address,uuid);			
			monotooth.Service.ServicePool pool = new monotooth.Service.ServicePool();
			monotooth.Service.Service serv = new monotooth.Service.Service();			
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
					serv = (monotooth.Service.Service) Marshal.PtrToStructure(element,typeof(monotooth.Service.Service));
					if(serv.rfcomm_port<32 && serv.rfcomm_port != 0 && serv.name.Length > 1)
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
			System.Text.StringBuilder bld = new System.Text.StringBuilder(18);			
			ba2str(this.address,bld);			
			return bld.ToString();
		}
		/// <summary>Returns a <c>monotooth.BluetoothAddress</c> from a string, using a native function. </summary>
		/// <returns>New address. </returns>
		/// <remarks>Will return an 0-address if the address string is not in the 48-bit form. </remarks>
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
		[DllImport("monotooth")]
		private static extern IntPtr search_services_from_with_uuid(monotooth.BluetoothAddress ba, uint uuid);
		[DllImport("bluetooth")]
		private static extern int ba2str(monotooth.BluetoothAddress ba, System.Text.StringBuilder bld);
		[DllImport("bluetooth")]
		private static extern IntPtr strtoba(string addr);
	}
}
