namespace GameServer.Config
{
    public struct ChannelStruct
    {
        public int Port;

        public string Name;

        public int Type;

        public int MaxUser;

        public ChannelStruct(int port, string name, int type, int max)
        {
            Port = port;
            Name = name;
            Type = type;
            MaxUser = max;
        }
    }
}
