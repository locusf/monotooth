
using System;

namespace monotooth.Model.Connections
{
	
	/// <summary>A HCI connection interface.</summary>
	/// <remarks>
	/// <para>It is highly rare that 2 devices communicate with the HCI interface, thus this
	/// interface is a little unneeded.</para>
	/// <para>In Linux, the application will need root-privileges to execute HCI connections.</para>
	/// </remarks>
	public interface HCIConnection : IConnection
	{
		bool Encryption
		{
			get;
			set;
		}
		bool Authentication
		{
			get;
			set;
		}
		bool Role
		{
			get;
			set;
		}
	}
}
