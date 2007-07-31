
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
        public int Read([In, Out] byte[] bytes)
        {
        	return Read(bytes,0,bytes.Length);
        }
        public override int Read([In, Out] byte[] buffer, int offset, int count)
        {
        	IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(buffer[0])*buffer.Length);
        	this.sock.ReadWithOffset(ptr,offset,count);        	
            Marshal.Copy(ptr,buffer,0,buffer.Length);
            return this.sock.BytesUsed;
        }
        
 		public void Write(byte[] buf)
 		{
 			Write(buf,0,buf.Length);
 		}
        public override void Write(byte[] buffer, int offset, int count)
        {
        	IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(buffer[0])*buffer.Length);
        	Marshal.Copy(buffer,0,ptr,buffer.Length);
        	this.sock.WriteWithOffset(ptr,offset,count);
        	
        }
        public override long Seek(long offset, SeekOrigin origin)
        {
        	// NetworkStream objects do not support seeking.
			throw new NotSupportedException ();
        	
        }
        public override void SetLength(long length)
        {
        	// NetworkStream objects do not support setting of length.			
			throw new NotSupportedException ();
        }
	}
}
