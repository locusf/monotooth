
using System;

namespace monotooth.Model.Device
{
	
	/// <summary>The superinterface for all devices. The sole purpose for this
	/// interface is to offer as much information as possible for the means 
	/// of using devices.</summary>
	public interface IDevice
	{
		/// <summary>The devices address. </summary>
		/// <value>A 48-bit bluetooth address. </value>
		monotooth.Model.BluetoothAddress Address 
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
		/// <summary>Returns the 48-bit bluetooth address of this device as a string .</summary>
		/// <returns>String presentation of this devices bluetooth address.</returns>
		string AddressAsString();		
		/// <summary>Converts a string presentation of a bluetooth address to a <c>BluetoothAddress</c>.</summary>
		/// <param name="addr"> A bluetooth address as string. </param>
		/// <returns>A bluetooth address wrapped to a class. </returns>
		monotooth.Model.BluetoothAddress StringAsAddress(string addr);
	}
}
