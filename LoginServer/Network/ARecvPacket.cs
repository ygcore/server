using Common.Utilities;
using System;
using System.Text;

namespace LoginServer.Network
{
    public abstract class ARecvPacket
    {
        private byte[] _buffer;
        private int _offset;
        protected Client _Client;

        protected internal Client GetClient()
        {
            return _Client;
        }

        protected internal byte[] GetBuffer()
        {
            return this._buffer;
        }

        protected internal void execute(Client Client, byte[] buffer)
        {
            _Client = Client;
            _buffer = buffer;
            _offset = 2;
            Read();
            Run();
        }

        protected internal int ReadD()
        {
            int num = BitConverter.ToInt32(this._buffer, this._offset);
            this._offset += 4;
            return num;
        }

        protected internal byte ReadC()
        {
            byte num = this._buffer[this._offset];
            ++this._offset;
            return num;
        }

        protected internal byte[] ReadB(int Length)
        {
            byte[] numArray = new byte[Length];
            Array.Copy((Array)this._buffer, this._offset, (Array)numArray, 0, Length);
            this._offset += Length;
            return numArray;
        }

        protected internal short ReadH()
        {
            short num = BitConverter.ToInt16(this._buffer, this._offset);
            this._offset += 2;
            return num;
        }

        protected internal double ReadF()
        {
            double num = BitConverter.ToDouble(this._buffer, this._offset);
            this._offset += 8;
            return num;
        }

        protected internal long ReadQ()
        {
            long num = BitConverter.ToInt64(this._buffer, this._offset);
            this._offset += 8;
            return num;
        }

        protected internal string ReadS(int Length)
        {
            string str = "";
            try
            {
                str = Encoding.Default.GetString(this._buffer, this._offset, Length);
                int length = str.IndexOf(char.MinValue);
                if (length != -1)
                    str = str.Substring(0, length);
                this._offset += Length;
            }
            catch (Exception ex)
            {
                Log.ErrorException("while reading string from packet", ex);
            }
            return str;
        }

        protected internal string ReadS()
        {
            string str = "";
            try
            {
                str = Encoding.Default.GetString(this._buffer, this._offset, this._buffer.Length - this._offset);
                int length = str.IndexOf(char.MinValue);
                if (length != -1)
                    str = str.Substring(0, length);
                this._offset += str.Length + 1;
            }
            catch (Exception ex)
            {
                Log.ErrorException("while reading string from packet", ex);
            }
            return str;
        }

        protected internal void Ignore(int in_offset)
        {
            this._offset = this._offset + in_offset;
            Log.Trace("Ignore {0} bytes", in_offset);
        }

        protected internal abstract void Read();

        protected internal abstract void Run();
    }
}
