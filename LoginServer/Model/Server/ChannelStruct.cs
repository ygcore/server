namespace LoginServer.Model.Server
{
    public struct ChannelStruct
    {
        public string Name;

        public int Port;

        public int Type;

        public int MaxUser;

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
