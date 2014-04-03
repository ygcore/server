namespace LoginServer.Config
{
    public struct NetworkStruct
    {
        public string PublicIp;

        public int PublicPort;

        public string PrivateIp;

        public int PrivatePort;

        public NetworkStruct(string ip1, int port1, string ip2, int port2)
        {
            PublicIp = ip1;
            PublicPort = port1;
            PrivateIp = ip2;
            PrivatePort = port2;
        }
    }
}
