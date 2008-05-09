
using System;

namespace monotooth.Connections
{
	
	/// A factory class that makes RFCOMM connections.
	public abstract class RFCommConnectionFactory
	{
		/// This method returns a specific rfcomm connection factory.
		/// <returns>OS-dependend RFCOMM connection factory</returns>
		public static RFCommConnectionFactory GetFactory()
		{
			int p = (int) System.Environment.OSVersion.Platform;
			if (( p == 128) || (p == 4))
			{
				return new LinuxRFCommConnectionFactory();
			} else 
			{
				return new WindowsRFCommConnectionFactory();
			}
		}
		/// An abstract method that is used to create RFCOMM connections
		public abstract monotooth.Connections.RFCommConnection CreateRFCommConnection(monotooth.BluetoothAddress from, monotooth.BluetoothAddress to);
	}
}
