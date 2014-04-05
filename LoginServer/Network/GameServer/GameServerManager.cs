using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Network.GameServer
{
    internal class GameServerManager
    {
        private static GameServerManager Instance = new GameServerManager();
        private List<GSClient> _Clients = new List<GSClient>();

        static GameServerManager()
        {
        }

        public GameServerManager()
        {
            Log.Info("GameServerManager Loaded");
        }

        public static GameServerManager GetInstance()
        {
            return GameServerManager.Instance;
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

        public List<GSClient> GetAllGSClient()
        {
            return _Clients;
        }
    }
}
