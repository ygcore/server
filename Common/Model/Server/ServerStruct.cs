using ProtoBuf;
using System.Collections.Generic;

namespace Common.Model.Server
{
    [ProtoContract]
    public struct ServerStruct
    {
        [ProtoMember(1)]
        public int ServerId;

        [ProtoMember(2)]
        public string ServerName;

        [ProtoMember(3)]
        public string ServerAddress;

        [ProtoMember(4)]
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
