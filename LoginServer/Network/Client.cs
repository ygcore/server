using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LoginServer.Network
{
    public class Client
    {
        public EndPoint _address;
        public TcpClient _client;
        public NetworkStream _stream;
        private byte[] _buffer;

        public Client(TcpClient client)
        {
            _client = client;
            _stream = client.GetStream();
            _address = client.Client.RemoteEndPoint;

            new Thread(new ThreadStart(BeginRead)).Start();
        }

        private void close()
        {
            ClientManager.GetInstance().RemoveClient(this);
            this._stream.Dispose();
        }

        private void BeginRead()
        {
            try
            {
                if (this._stream == null || !this._stream.CanRead)
                    return;

                _buffer = new byte[1024];
                _stream.BeginRead(_buffer, 0, _buffer.Length, new AsyncCallback(OnReceiveCallback), (object)null);
            }
            catch (Exception ex)
            {
                Log.ErrorException("[Client]: BeginRead() Exception", ex);
                close();
            }
        }

        private void OnReceiveCallback(IAsyncResult ar)
        {
            int length = _stream.EndRead(ar);
            byte[] data = new byte[length];
            Buffer.BlockCopy(_buffer, 0, data, 0, length);

            if (data.Length >= 2)
                handlePacket(data);

            new Thread(new ThreadStart(BeginRead)).Start();
        }

        private void handlePacket(byte[] Data)
        {
            Log.Debug("Recv Handle: {0}", Data.FormatHex());
            short opcode = BitConverter.ToInt16(new byte[2] { Data[0], Data[1] }, 0);

            if (Opcode.Recv.ContainsKey(opcode))
            {
                ((ARecvPacket)Activator.CreateInstance(Opcode.Recv[opcode])).execute(this, Data);
            }
            else
            {
                string opCodeLittleEndianHex = BitConverter.GetBytes(opcode).ToHex();
                Log.Debug("Unknown Opcode: 0x{0}{1} [{2}]",
                                 opCodeLittleEndianHex.Substring(2),
                                 opCodeLittleEndianHex.Substring(0, 2),
                                 Data.Length);

                Log.Debug("Data:\n{0}", Data.FormatHex());
            }
        }
    }
}
