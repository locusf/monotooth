
using System;

namespace monotooth.Model.Connections
{
	
	
	public class LinuxRFCommConnectionFactory : RFCommConnectionFactory
	{
		
		public LinuxRFCommConnectionFactory()
		{
		}
		public override monotooth.Model.Connections.RFCommConnection CreateRFCommConnection(monotooth.Model.BluetoothAddress from, monotooth.Model.BluetoothAddress to)
		{
			LinuxRFCommConnection conn = new LinuxRFCommConnection();
			conn.from = from;
			conn.to = to;
			return conn;
		}
	}
}
