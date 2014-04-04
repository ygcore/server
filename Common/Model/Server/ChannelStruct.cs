using ProtoBuf;

namespace Common.Model.Server
{
    [ProtoContract]
    public struct ChannelStruct
    {
        [ProtoMember(1)]
        public string Name;

        [ProtoMember(2)]
        public int Port;

        [ProtoMember(3)]
        public int Type;

        [ProtoMember(4)]
        public int MaxUser;

        [ProtoMember(5)]
        public int CurrentUser;

        public ChannelStruct(string name, int port, int type, int maxuser)
        {
            Name = name;
            Port = port;
            Type = type;
            MaxUser = maxuser;
            CurrentUser = 0;
        }
    }
}
