using System;
using System.IO;
using System.Text;

namespace LoginServer.Network.GameServer
{
    public abstract class GSASendPacket
    {
        private MemoryStream mstream = new MemoryStream();

        public GSClient _Client;

        public long Length
        {
            get
            {
                return this.mstream.Length;
            }
        }

        protected internal void WriteB(byte[] value)
        {
            this.mstream.Write(value, 0, value.Length);
        }

        protected internal void WriteD(int value)
        {
            WriteB(BitConverter.GetBytes(value));
        }

        protected internal void WriteH(short val)
        {
            WriteB(BitConverter.GetBytes(val));
        }

        protected internal void WriteH(int val)
        {
            WriteB(BitConverter.GetBytes((short)val));
        }

        protected internal void WriteC(byte value)
        {
            this.mstream.WriteByte(value);
        }

        protected internal void WriteC(int value)
        {
            this.mstream.WriteByte((byte)value);
        }

        protected internal void WriteF(double value)
        {
            WriteB(BitConverter.GetBytes(value));
        }

        protected internal void WriteQ(long value)
        {
            WriteB(BitConverter.GetBytes(value));
        }

        protected internal void WriteS(string value)
        {
            if (value == null)
                return;

            WriteH((short)value.Length);
            WriteB(Encoding.Default.GetBytes(value));
        }

        protected internal void WriteS(string name, int count)
        {
            if (name == null)
                return;
            WriteB(Encoding.Default.GetBytes(name));
            WriteB(new byte[count - name.Length]);
        }

        public byte[] ToByteArray()
        {
            return this.mstream.ToArray();
        }

        protected internal abstract void Write();
    }
}
