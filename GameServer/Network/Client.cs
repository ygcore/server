using Common.Utilities;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GameServer.Network
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
            byte[] data = new byte[length - 4];
            Buffer.BlockCopy(_buffer, 2, data, 0, length - 4);

            string bytesString = BitConverter.ToString(data).Replace("-", "");
            string[] delimiters = new string[] { "55AAAA55" };
            string[] strArray = bytesString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            foreach (string str in strArray)
            {
                handlePacket(str.ToBytes());
            }

            new Thread(new ThreadStart(BeginRead)).Start();
        }

        public void Send(byte[] bytes)
        {
            _stream = _client.GetStream();
            _stream.BeginWrite(bytes, 0, bytes.Length, WriteCallback, null);
        }

        public void SendPacket(ASendPacket packet)
        {
            packet._Client = this;

            if (!Opcode.Send.ContainsKey(packet.GetType()))
            {
                Log.Warn("UNKNOWN GS packet opcode: {0}", packet.GetType().Name);
                return;
            }

            try
            {
                packet.WriteH(Opcode.Send[packet.GetType()]); // opcode
                packet.WriteH(0); // packet len
                packet.Write();

                byte[] Data = packet.ToByteArray();
                BitConverter.GetBytes((short)(Data.Length - 4)).CopyTo(Data, 2);

                //if(Configuration.Setting.Debug) Log.Debug("Send: {0}", Data.FormatHex());
                _stream = _client.GetStream();
                _stream.BeginWrite(Data, 0, Data.Length, new AsyncCallback(WriteCallback), (object)null);
            }
            catch (Exception ex)
            {
                Log.Warn("Can't send GS packet: {0}", GetType().Name);
                Log.WarnException("GSASendPacket", ex);
                return;
            }
        }

        private void WriteCallback(IAsyncResult result)
        {
            _stream.EndWrite(result);
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
