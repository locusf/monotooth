
using System;

namespace monotooth.Device
{
	
	/// <summary>The superinterface for all devices. The sole purpose for this
	/// interface is to offer as much information as possible for the means 
	/// of using devices.</summary>
	public interface IDevice
	{
		/// <summary>The devices address. </summary>
		/// <value>A 48-bit bluetooth address. </value>
		monotooth.BluetoothAddress Address 
		{
			get;
			set;
		}
		/// <summary>The friendly name of this device (the human readable form). </summary>
		/// <value>A string. </value>
		string FriendlyName
		{
			get;
			set;
		}
	}
}
