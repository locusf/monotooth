
using System;

namespace monotooth.Connections
{
	
	
	public class LinuxServiceConnectionFactory : ServiceConnectionFactory
	{
		
		public LinuxServiceConnectionFactory()
		{
		}
		public override monotooth.Connections.ServiceConnection CreateServiceConnection(monotooth.Connections.RFCommConnection conn)
		{
			return new LinuxServiceConnection(conn);
		}
		
	}
}
