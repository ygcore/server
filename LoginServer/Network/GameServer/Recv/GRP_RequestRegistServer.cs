using Common.Utilities;
using LoginServer.Model.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Network.GameServer.Recv
{
    public class GRP_RequestRegistServer : GSARecvPacket
    {
        protected int serverId;
        protected internal override void Read()
        {
            serverId = ReadD();
            string serverName = ReadS();
            string serverAddr = ReadS();

            ServerStruct server = new ServerStruct(serverId, serverName, serverAddr);

            int channel_count = ReadD();
            for(int i = 0; i < channel_count; i++)
            {
                string name = ReadS();
                int port = ReadH();
                int type = ReadC();
                int max = ReadD();
                ChannelStruct channel = new ChannelStruct(name, port, type, max);
                server.Channels.Add(channel);
            }

            LoginServer.ServerList.Add(server);
        }

        protected internal override void Run()
        {
            Log.Info("Registered Server ID: {0}", serverId);
        }
    }
}
