
using System;
using System.Runtime.InteropServices;
namespace monotooth.Model.Service
{
		/// <summary>A structure to describe a service.</summary>
		[StructLayout (LayoutKind.Sequential, Pack = 1)]
		public struct Service
		{
		/// <summary>A port of the service. </summary>
		public int rfcomm_port;
		/// <summary>Name of the service. </summary>
		[MarshalAs(UnmanagedType.ByValTStr,SizeConst=256)]
		public string name;
		/// <summary>Description of the service.</summary>
		[MarshalAs(UnmanagedType.ByValTStr,SizeConst=256)]
		public string description;
		}
	
	
}
