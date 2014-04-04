using Common.Utilities;
using GameServer.Config;
using GameServer.Network.LoginServer.Send;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace GameServer.Network.LoginServer
{
    public class LSClient
    {
        private static LSClient Instance;

        public TcpClient _client;
        public NetworkStream _stream;
        private byte[] _buffer;
        private int failedConnectionCount;

        public static LSClient GetInstance()
        {
            return (Instance != null) ? Instance : Instance = Instance = new LSClient();
        }

        public LSClient()
        {
            LSOpcode.Init();
            _client = new TcpClient();
            _client.BeginConnect(Configuration.Network.LoginIp, Configuration.Network.LoginPort, ConnectCallback, null);
        }

        private void ConnectCallback(IAsyncResult result)
        {
            try
            {
                _client.EndConnect(result);

                SendPacket(new GSReqRegistServer());
            }
            catch
            {
                Interlocked.Increment(ref failedConnectionCount);
                if (failedConnectionCount >= 5)
                    return; //connection has failed overall.
            }

            NetworkStream networkStream = _client.GetStream();
            _buffer = new byte[_client.ReceiveBufferSize];
            networkStream.BeginRead(_buffer, 0, _buffer.Length, ReadCallback, _buffer);
        }

        private void ReadCallback(IAsyncResult result)
        {
            int length = 0;
            try
            {
                _stream = _client.GetStream();
                if (_stream != null)
                    length = _stream.EndRead(result);
            }
            catch
            {
                Log.Error("Lost connection from loginserver");
                return;
            }

            if (length == 0)
            {
                return;
            }

            byte[] data = new byte[length];
            Buffer.BlockCopy(_buffer, 0, data, 0, length);

            HandlePacket(data);

            _stream.BeginRead(_buffer, 0, _buffer.Length, ReadCallback, _buffer);
        }

        public void Send(byte[] bytes)
        {
            _stream = _client.GetStream();
            _stream.BeginWrite(bytes, 0, bytes.Length, WriteCallback, null);
        }

        public void SendPacket(LSASendPacket packet)
        {
            packet._Client = this;

            if (!LSOpcode.Send.ContainsKey(packet.GetType()))
            {
                Log.Warn("UNKNOWN GS packet opcode: {0}", packet.GetType().Name);
                return;
            }

            try
            {
                packet.WriteH(LSOpcode.Send[packet.GetType()]); // opcode
                packet.WriteH(0); // packet len
                packet.Write();

                byte[] Data = packet.ToByteArray();
                BitConverter.GetBytes((short)(Data.Length - 4)).CopyTo(Data, 2);

                //if (Configuration.Setting.Debug) Log.Debug("Send: {0}", Data.FormatHex());
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

        private void HandlePacket(byte[] Data)
        {
            //Log.Debug("Recv Handle: {0}", _buffer.FormatHex());

            short opcode = BitConverter.ToInt16(new byte[2] { Data[0], Data[1] }, 0);

            if (LSOpcode.Recv.ContainsKey(opcode))
            {
                ((LSARecvPacket)Activator.CreateInstance(LSOpcode.Recv[opcode])).execute(this, Data);
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
