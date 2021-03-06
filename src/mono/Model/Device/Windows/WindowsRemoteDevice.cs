/*
 * Created by SharpDevelop.
 * User: LoCusF
 * Date: 19.4.2008
 * Time: 12:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace monotooth.Device
{
	/// <summary>
	/// Description of WindowsRemoteDevice.
	/// </summary>
	public class WindowsRemoteDevice: IRemoteDevice
	{
		public WindowsRemoteDevice(monotooth.BluetoothAddress ba, String name)			
		{
			this.address = ba;
			this.name = name;
		}
		private monotooth.BluetoothAddress address;
		private String name;
		public monotooth.Service.ServicePool Services {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public BluetoothAddress Address {
			get {
				return this.address;
			}
			set {
				this.address = value;
			}
		}
		
		public string FriendlyName {
			get {
				return this.name;
			}
			set {
				this.name = value;
			}
		}
		
		public monotooth.Service.ServicePool InquireServices(uint uuid)
		{
			throw new NotImplementedException();
		}
		
	}
}
