using Common.Utilities;
using GameServer.Config;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace GameServer.Network.LoginServer
{
    public class LSNetworkClient
    {
        private static LSNetworkClient Instance;

        public TcpClient _client;
        public NetworkStream _stream;
        private byte[] _buffer;
        private int failedConnectionCount;

        public static LSNetworkClient GetInstance()
        {
            return (Instance != null) ? Instance : Instance = Instance = new LSNetworkClient();
        }

        public LSNetworkClient()
        {
            _client = new TcpClient();
            _client.BeginConnect(Configuration.Network.LoginIp, Configuration.Network.LoginPort, ConnectCallback, null);
        }

        private void ConnectCallback(IAsyncResult result)
        {
            try
            {
                Log.Debug("Connected to LoginServer");
                Send(Encoding.UTF8.GetBytes("TESTTESTTESTTEST"));
                _client.EndConnect(result);
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

        private void WriteCallback(IAsyncResult result)
        {
            _stream.EndWrite(result);
        }

        private void HandlePacket(byte[] _buffer)
        {
            Log.Debug("Recv Handle: {0}", _buffer.FormatHex());
        }
    }
}
