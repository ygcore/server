namespace Common.Model.Server
{
    public struct ChannelStruct
    {
        public int Id;

        public string Name;

        public int Port;

        public int Type;

        public int MaxUser;

        public int CurrentUser;

        public ChannelStruct(int id, string name, int port, int type, int maxuser, int curr = 0)
        {
            Id = id;
            Name = name;
            Port = port;
            Type = type;
            MaxUser = maxuser;
            CurrentUser = curr;
        }
    }
}
