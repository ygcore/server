using Common.Model.Server;
using Common.Utilities;

namespace LoginServer.Network.GameServer.Recv
{
    public class GSResRegistServer : GSARecvPacket
    {
        protected int serverId;
        protected internal override void Read()
        {
            serverId = ReadD();
            string serverName = ReadS();
            string serverAddr = ReadS();
            bool useAkey = (ReadC() == 1) ? true : false;
            ServerStruct server = new ServerStruct(serverId, serverName, serverAddr, useAkey);

            int channel_count = ReadD();
            for(int i = 0; i < channel_count; i++)
            {
                int id = ReadH();
                string name = ReadS();
                int port = ReadH();
                int type = ReadC();
                int max = ReadD();
                int curr = ReadD();
                ChannelStruct channel = new ChannelStruct(id, name, port, type, max, curr);
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
