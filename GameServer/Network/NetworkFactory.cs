using Common.Utilities;
using GameServer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer.Network
{
    internal class NetworkFactory
    {
        private static List<NetworkFactory> Instances = new List<NetworkFactory>();

        private TcpListener NetworkListener;

        public NetworkFactory(int port)
        {
            new Thread(new ParameterizedThreadStart(NetworkStart)).Start(port);
        }

        public static void NewInstance(int port)
        {
            Instances.Add(new NetworkFactory(port));
        }

        private void NetworkStart(object parameter)
        {
            try
            {
                int port = (int)parameter;
                Opcode.Init();
                NetworkListener = new TcpListener(IPAddress.Parse(Configuration.Network.PublicIp), port);
                NetworkListener.Start();
                Log.Info("Server listening clients at {0}:{1}...", ((IPEndPoint)NetworkListener.LocalEndpoint).Address, port);
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
