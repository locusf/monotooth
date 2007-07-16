
using System;

namespace monotooth.Model.Connections
{
	
	
	public class WindowsRFCommConnectionFactory : RFCommConnectionFactory
	{
		
		public WindowsRFCommConnectionFactory()
		{
		}
		public override monotooth.Model.Connections.RFCommConnection CreateRFCommConnection(monotooth.Model.BluetoothAddress from, monotooth.Model.BluetoothAddress to)
		{
			return new WindowsRFCommConnection();
		}
	}
}
