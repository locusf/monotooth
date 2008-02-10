
using System;

namespace monotooth.Connections
{
	
	
	public class WindowsRFCommConnectionFactory : RFCommConnectionFactory
	{
		
		public WindowsRFCommConnectionFactory()
		{
		}
		public override monotooth.Connections.RFCommConnection CreateRFCommConnection(monotooth.BluetoothAddress from, monotooth.BluetoothAddress to)
		{
			return new WindowsRFCommConnection();
		}
	}
}
