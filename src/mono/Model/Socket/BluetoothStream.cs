
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace monotooth.Model.Socket
{
	
	/// <summary>A simple stream to handle bluetooth streams. </summary>
	public class BluetoothStream : Stream
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
            get { return 0; }
        }
        public override long Position {
                 get { return 0; }
                 set {}
        }
        public override void Close()
        {
        	this.sock.disconnect();
        }
        public override void Flush()
        {
        }
        private System.Text.StringBuilder ConvertBytesToStringBuilder(byte[] buf)
        {
        	System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
        	return new System.Text.StringBuilder(enc.GetString(buf));
        }
        private byte[] ConvertStringBuilderToBytes(System.Text.StringBuilder bld)
        {
        	System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
        	return enc.GetBytes(bld.ToString());
        }
        public int Read([In, Out] byte[] bytes)
        {
        	return Read(bytes,0,bytes.Length);
        }
        public override int Read([In, Out] byte[] buffer, int offset, int count)
        {
        	System.Text.StringBuilder bld = ConvertBytesToStringBuilder(buffer);
        	this.sock.Read(bld);
        	buffer = ConvertStringBuilderToBytes(bld);
            return this.sock.BytesUsed;
        }
 		public void Write(byte[] buf)
 		{
 			Write(buf,0,buf.Length);
 		}
        public override void Write(byte[] buffer, int offset, int count)
        {
        	System.Text.StringBuilder bld = ConvertBytesToStringBuilder(buffer);
        	this.sock.Write(bld);
        	
        }
        public override long Seek(long offset, SeekOrigin origin)
        {
        	return 0;
        }
        public override void SetLength(long length)
        {
        }
	}
}
