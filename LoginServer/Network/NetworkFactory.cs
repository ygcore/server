using Common.Utilities;
using LoginServer.Config;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace LoginServer.Network
{
    internal class NetworkFactory
    {
        private static NetworkFactory Instance;

        private static TcpListener NetworkListener;

        public NetworkFactory()
        {
            new Thread(new ThreadStart(NetworkStart)).Start();
        }

        public static NetworkFactory GetInstance()
        {
            return (Instance != null) ? Instance : Instance = Instance = new NetworkFactory();
        }

        private void NetworkStart()
        {
            try
            {
                NetworkListener = new TcpListener(IPAddress.Parse(Configuration.Network.PublicIp), Configuration.Network.PublicPort);
                NetworkListener.Start();
                Log.Info("Server listening clients at {0}:{1}...", ((IPEndPoint)NetworkListener.LocalEndpoint).Address, Configuration.Network.PublicPort);
                NetworkListener.BeginAcceptTcpClient(new AsyncCallback(BeginAcceptTcpClient), (object)null);
            }
            catch (Exception ex)
            {
                Log.ErrorException("NetworkStart:", ex);
            }
        }

        private void BeginAcceptTcpClient(IAsyncResult ar)
        {
            Accept(NetworkListener.EndAcceptTcpClient(ar));
            NetworkListener.BeginAcceptTcpClient(new AsyncCallback(this.BeginAcceptTcpClient), (object)null);
        }

        private void Accept(TcpClient tcpClient)
        {
            ClientManager.GetInstance().AddClient(tcpClient);
        }
    }
}
