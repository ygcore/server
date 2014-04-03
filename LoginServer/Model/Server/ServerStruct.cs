using System.Collections.Generic;

namespace LoginServer.Model.Server
{
    public struct ServerStruct
    {
        public int ServerId;

        public string ServerName;

        public string ServerAddress;

        public List<ChannelStruct> Channels;

        public ServerStruct(int id, string name, string addr)
        {
            ServerId = id;
            ServerName = name;
            ServerAddress = addr;
            Channels = new List<ChannelStruct>();
        }
    }
}
