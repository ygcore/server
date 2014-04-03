using Common.Utilities;
using System.Collections.Generic;
using System.Net.Sockets;

namespace LoginServer.Network
{
    internal class ClientManager
    {
        private static ClientManager Instance = new ClientManager();
        private List<Client> _Clients = new List<Client>();

        static ClientManager()
        {
        }

        public ClientManager()
        {
            Log.Info("ClientManager Loaded");
        }

        public static ClientManager GetInstance()
        {
            return ClientManager.Instance;
        }

        public void AddClient(TcpClient tcp)
        {
            // todo block ip
            string ip = tcp.Client.RemoteEndPoint.ToString().Split(':')[0];


            Client client = new Client(tcp);
            if (_Clients.Contains(client))
                Log.Warn("Client is already exists!");
            else
                _Clients.Add(client);
        }

        public void RemoveClient(Client loginClient)
        {
            if (!this._Clients.Contains(loginClient))
                return;

            this._Clients.Remove(loginClient);
        }
    }
}
