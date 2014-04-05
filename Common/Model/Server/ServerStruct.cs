using System.Collections.Generic;

namespace Common.Model.Server
{
    public struct ServerStruct
    {
        public int Id;

        public string Name;

        public string Address;

        public bool IsUseAtKey;

        public Dictionary<int, ChannelStruct> Channels;

        public ServerStruct(int id, string name, string addr, bool usekey)
        {
            Id = id;
            Name = name;
            Address = addr;
            IsUseAtKey = usekey;
            Channels = new Dictionary<int, ChannelStruct>();
        }
    }
}
