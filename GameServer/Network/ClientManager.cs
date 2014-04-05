using Common.Model.Server;
using Common.Utilities;
using GameServer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace GameServer.Network
{
    internal class ClientManager
    {
        private static ClientManager Instance = new ClientManager();
        private Dictionary<int, List<Client>> _Clients = new Dictionary<int, List<Client>>();

        static ClientManager()
        {
        }

        public ClientManager()
        {
            foreach(ChannelStruct chn in Configuration.GetInstance().Channels)
            {
                _Clients.Add(chn.Id, new List<Client>());
            }  
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
            int local_port = Convert.ToInt32(tcp.Client.LocalEndPoint.ToString().Split(':')[1]);

            int chnId = Configuration.GetInstance().Channels
                .Where(c => c.Port == local_port)
                .Select(v => v.Id).FirstOrDefault();

            Client client = new Client(tcp);
            
            if (_Clients[chnId].Contains(client))
                Log.Warn("Client is already exists!");
            else
                _Clients[chnId].Add(client);
        }

        public void RemoveClient(Client loginClient)
        {
            int local_port = Convert.ToInt32(loginClient._client.Client.LocalEndPoint.ToString().Split(':')[1]);
            int chnId = Configuration.GetInstance().Channels
                .Where(c => c.Port == local_port)
                .Select(v => v.Id).FirstOrDefault();

            if (!_Clients[chnId].Contains(loginClient))
                return;

            _Clients[chnId].Remove(loginClient);
        }

        public int GetUserOnlineCount(int channelId)
        {
            return _Clients[channelId].Count;
        }
    }
}
