
using System;

namespace monotooth.Connections
{
	
	/// An abstract class for creating service connection factories
	public abstract class ServiceConnectionFactory
	{
		/// <c>GetFactory</c> method creates an OS specific factory for service connections.
		public static ServiceConnectionFactory GetFactory()
		{
			int p = (int) System.Environment.OSVersion.Platform;
			if (( p == 128) || (p == 4))
			{
				return new LinuxServiceConnectionFactory();
			}
			else
			{
				return null;
			}
		}
		/// An abstract method to create service connections
		/// <returns>OS-specific service connection</returns>
		public abstract monotooth.Connections.ServiceConnection CreateServiceConnection(monotooth.Connections.RFCommConnection conn);
	}
}
