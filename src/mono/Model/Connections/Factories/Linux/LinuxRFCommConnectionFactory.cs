
using System;

namespace monotooth.Connections
{
	
	
	public class LinuxRFCommConnectionFactory : RFCommConnectionFactory
	{
		
		public LinuxRFCommConnectionFactory()
		{
		}
		public override monotooth.Connections.RFCommConnection CreateRFCommConnection(monotooth.BluetoothAddress from, monotooth.BluetoothAddress to)
		{
			LinuxRFCommConnection conn = new LinuxRFCommConnection();
			conn.from = from;
			conn.to = to;
			return conn;
		}
	}
}
