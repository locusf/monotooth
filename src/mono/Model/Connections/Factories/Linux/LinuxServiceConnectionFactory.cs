
using System;

namespace monotooth.Model.Connections
{
	
	
	public class LinuxServiceConnectionFactory : ServiceConnectionFactory
	{
		
		public LinuxServiceConnectionFactory()
		{
		}
		public override monotooth.Model.Connections.ServiceConnection CreateServiceConnection(monotooth.Model.Connections.RFCommConnection conn)
		{
			return new LinuxServiceConnection(conn);
		}
		
	}
}
