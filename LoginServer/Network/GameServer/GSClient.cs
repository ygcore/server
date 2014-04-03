using Common.Utilities;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LoginServer.Network.GameServer
{
    public class GSClient
    {
        public EndPoint _address;
        public TcpClient _client;
        public NetworkStream _stream;
        private byte[] _buffer;

        public GSClient(TcpClient client)
        {
            _client = client;
            _stream = client.GetStream();
            _address = client.Client.RemoteEndPoint;

            new Thread(new ThreadStart(BeginRead)).Start();
        }

        private void close()
        {
            GSClientManager.GetInstance().RemoveClient(this);
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
                Log.ErrorException("[GSClient]: BeginRead() Exception", ex);
                close();
            }
        }

        private void OnReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int length = _stream.EndRead(ar);
                byte[] data = new byte[length];
                Buffer.BlockCopy(_buffer, 0, data, 0, length);

                if (data.Length >= 2)
                    handlePacket(data);

                new Thread(new ThreadStart(BeginRead)).Start();
            }
            catch
            {
                GSClientManager.GetInstance().RemoveClient(this);
                Log.Error("Lost connection from gameserver");
                //Log.ErrorException("OnReceiveCallback", ex);
            }
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

        private void handlePacket(byte[] data)
        {
            Log.Debug("Recv Handle: {0}", data.FormatHex());
            Send(Encoding.UTF8.GetBytes("TEST SEND BACK"));
        }
    }
}
