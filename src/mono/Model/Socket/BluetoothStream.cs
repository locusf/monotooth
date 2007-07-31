
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace monotooth.Model.Socket
{
	
	/// <summary>A simple stream to handle bluetooth streams. </summary>
	public class BluetoothStream : Stream, IDisposable
	{
		private monotooth.Model.Connections.RFCommConnection sock;
		private BluetoothStream()
		{
		}		
		/// <summary>The default constructor.</summary>
		/// <param name="conn">The used connection that this stream will use. </param>
		public BluetoothStream(monotooth.Model.Connections.RFCommConnection conn)
		{
			this.sock = conn;
		}
		public override bool CanRead {
			get { return true; }
        }
        public override bool CanWrite {
            get { return true; }
        }
        public override bool CanSeek {
            get { return false; }
        }
        public override long Length {
            get { throw new NotSupportedException (); }
        }
        public override long Position {
                 get { throw new NotSupportedException (); }
                 set { throw new NotSupportedException (); }
        }
        public override void Close()
        {
        	((IDisposable) this).Dispose ();        	
        }
        protected virtual void Dispose(bool disposing)
        {
        	this.sock.disconnect();
        }
        public override void Flush()
        {
        }
        void IDisposable.Dispose ()
		{
			Dispose (true);		
		}        
        
        public override int Read([In, Out] byte[] buffer, int offset, int count)
        {
        	if (offset < 0) throw new ArgumentException("offset","May not be less than zero!");
        	if (offset > buffer.Length) throw new ArgumentException("offset","Trying to read outside of buffer!");        	
        	IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(buffer[0])*buffer.Length);
        	this.sock.ReadWithOffset(ptr,offset,count);        	
            Marshal.Copy(ptr,buffer,0,buffer.Length);
            Marshal.FreeHGlobal(ptr);
            return this.sock.BytesUsed;
        }
        		
        public override void Write(byte[] buffer, int offset, int count)
        {
        	if (offset < 0) throw new ArgumentException("offset","May not be less than zero!");
        	if (offset > buffer.Length) throw new ArgumentException("offset","Trying to write to outside of buffer!");
        	IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(buffer[0])*buffer.Length);
        	Marshal.Copy(buffer,0,ptr,buffer.Length);
        	this.sock.WriteWithOffset(ptr,offset,count);
        	Marshal.FreeHGlobal(ptr);
        }
        public override long Seek(long offset, SeekOrigin origin)
        {        	
			throw new NotSupportedException ();        	
        }
        public override void SetLength(long length)
        {        	
			throw new NotSupportedException ();
        }
	}
}
