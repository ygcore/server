using Common.Model.Server;
using System.Linq;

namespace LoginServer.Network.Send
{
    public class ResponseSelectServer : ASendPacket
    {
        protected ServerStruct server;
        protected ChannelStruct channel;

        public ResponseSelectServer(int sid, int cid)
        {
            server = LoginServer.ServerList[sid];
            channel = server.Channels[cid];
        }

        protected internal override void Write()
        {
            WriteS(server.Address);
            WriteH(channel.Port);
            WriteS(channel.Name);
            WriteS(channel.Name);
        }
    }
}
