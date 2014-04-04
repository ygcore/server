using Common.Model.Server;

namespace LoginServer.Network.Send
{
    public class ResponseServerList : ASendPacket
    {
        protected internal override void Write()
        {
            WriteH(LoginServer.ServerList.Count);

            foreach(ServerStruct server in LoginServer.ServerList)
            {
                WriteH(server.Id);
                WriteS(server.Name);
                WriteH(0);
                WriteH(0);
                WriteH(1);
                WriteH(server.Channels.Count);
                for(int i = 0; i < server.Channels.Count; i++)
                {
                    var channel = server.Channels[i];
                    int online = 0; // todo get user online in channel
                    int percent = ((online * 100) / channel.MaxUser);
                    WriteH((i + 1));
                    WriteS(channel.Name);
                    WriteH(percent);
                    WriteH(channel.Type);
                }
            }
        }
    }
}
