/*
 * Created by SharpDevelop.
 * User: LoCusF
 * Date: 19.4.2008
 * Time: 15:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace monotooth.Connections
{
	/// <summary>
	/// Description of WindowsServiceConnection.
	/// </summary>
	public class WindowsServiceConnection : ServiceConnection
	{
		public WindowsServiceConnection()
		{
		}
		
		public RFCommConnection Connection {
			get {
				throw new NotImplementedException();
			}
		}
		
		public int SocketDescriptor {
			get {
				throw new NotImplementedException();
			}
		}
		
		public monotooth.BluetoothAddress from {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public BluetoothAddress to {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public void connect(uint uuid)
		{
			throw new NotImplementedException();
		}
		
		public void RegisterService(string name, string description, string vendor, int rfcomm_channel, uint uuid)
		{
			throw new NotImplementedException();
		}
		
		public void connect()
		{
			throw new NotImplementedException();
		}
		
		public void disconnect()
		{
			throw new NotImplementedException();
		}
		
		public bool isConnected()
		{
			throw new NotImplementedException();
		}
		
		public void Write(System.Text.StringBuilder bytes)
		{
			throw new NotImplementedException();
		}
		
		public void Read(System.Text.StringBuilder bytes)
		{
			throw new NotImplementedException();
		}
	}
}
