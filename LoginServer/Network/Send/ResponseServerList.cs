using Common.Model.Server;
using Common.Utilities;
using System.Linq;

namespace LoginServer.Network.Send
{
    public class ResponseServerList : ASendPacket
    {
        protected internal override void Write()
        {
            WriteH(LoginServer.ServerList.Count);

            foreach(ServerStruct server in LoginServer.ServerList.Values)
            {
                WriteH(server.Id);
                WriteS(server.Name);
                WriteH(0);
                WriteH(0);
                WriteH(1);
                WriteH(server.Channels.Count);
                foreach (var channel in server.Channels.Values.ToList())
                {
                    int online = channel.CurrentUser; // todo get user online in channel
                    int percent = ((online * 100) / channel.MaxUser);
                    WriteH(channel.Id);
                    WriteS(channel.Name);
                    WriteH(percent);
                    WriteH(channel.Type);
                }
            }
        }
    }
}
