using Common.Utilities;
using LoginServer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoginServer.Network.GameServer
{
    internal class GSNetworkFactory
    {
        private static GSNetworkFactory Instance;

        private static TcpListener NetworkListener;

        public GSNetworkFactory()
        {
            new Thread(new ThreadStart(NetworkStart)).Start();
        }

        public static GSNetworkFactory GetInstance()
        {
            return (Instance != null) ? Instance : Instance = Instance = new GSNetworkFactory();
        }

        private void NetworkStart()
        {
            try
            {
                GSOpcode.Init();
                NetworkListener = new TcpListener(IPAddress.Parse(Configuration.Network.PrivateIp), Configuration.Network.PrivatePort);
                NetworkListener.Start();
                Log.Info("Server listening gs clients at {0}:{1}...", ((IPEndPoint)NetworkListener.LocalEndpoint).Address, Configuration.Network.PrivatePort);
                NetworkListener.BeginAcceptTcpClient(new AsyncCallback(BeginAcceptTcpClient), (object)null);
            }
            catch (Exception ex)
            {
                Log.ErrorException("GS NetworkStart:", ex);
            }
        }

        private void BeginAcceptTcpClient(IAsyncResult ar)
        {
            Accept(NetworkListener.EndAcceptTcpClient(ar));
            NetworkListener.BeginAcceptTcpClient(new AsyncCallback(this.BeginAcceptTcpClient), (object)null);
        }

        private void Accept(TcpClient tcpClient)
        {
            Log.Debug("Recieve connection from GameServer");
            GSClientManager.GetInstance().AddClient(tcpClient);
        }
    }
}
