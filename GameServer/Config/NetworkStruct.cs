namespace GameServer.Config
{
    public struct NetworkStruct
    {
        public string PublicIp;

        public string PrivateIp;

        public int PrivatePort;

        public string LoginIp;

        public int LoginPort;

        public NetworkStruct(string ip1, string ip2, int port2, string ip3, int port3)
        {
            PublicIp = ip1;
            PrivateIp = ip2;
            PrivatePort = port2;
            LoginIp = ip3;
            LoginPort = port3;
        }
    }
}
