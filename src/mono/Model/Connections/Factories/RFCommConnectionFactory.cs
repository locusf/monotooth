
using System;

namespace monotooth.Model.Connections
{
	
	/// A factory class that makes RFCOMM connections.
	public abstract class RFCommConnectionFactory
	{
		/// This method returns a specific rfcomm connection factory.
		/// <returns>OS-dependend RFCOMM connection factory</returns>
		public static RFCommConnectionFactory GetFactory()
		{
			if ((System.Environment.OSVersion.Platform.value__ == 128) || (System.Environment.OSVersion.Platform.value__ == 4))
			{
				return new LinuxRFCommConnectionFactory();
			} else 
			{
				return new WindowsRFCommConnectionFactory();
			}
		}
		/// An abstract method that is used to create RFCOMM connections
		public abstract monotooth.Model.Connections.RFCommConnection CreateRFCommConnection(monotooth.Model.BluetoothAddress from, monotooth.Model.BluetoothAddress to);
	}
}
