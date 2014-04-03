using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Network.GameServer
{
    internal class GSClientManager
    {
        private static GSClientManager Instance = new GSClientManager();
        private List<GSClient> _Clients = new List<GSClient>();

        static GSClientManager()
        {
        }

        public GSClientManager()
        {
            Log.Info("ClientManager Loaded");
        }

        public static GSClientManager GetInstance()
        {
            return GSClientManager.Instance;
        }

        public void AddClient(TcpClient tcp)
        {
            // todo block ip
            string ip = tcp.Client.RemoteEndPoint.ToString().Split(':')[0];


            GSClient client = new GSClient(tcp);
            if (_Clients.Contains(client))
                Log.Warn("Client is already exists!");
            else
                _Clients.Add(client);
        }

        public void RemoveClient(GSClient loginClient)
        {
            if (!this._Clients.Contains(loginClient))
                return;

            this._Clients.Remove(loginClient);
        }
    }
}
