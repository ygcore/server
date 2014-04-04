using LoginServer.Network.GameServer.Recv;
using LoginServer.Network.GameServer.Send;
using System;
using System.Collections.Generic;

namespace LoginServer.Network.GameServer
{
    public class GSOpcode
    {
        public static Dictionary<short, Type> Recv = new Dictionary<short, Type>();
        public static Dictionary<Type, short> Send = new Dictionary<Type, short>();

        public static void Init()
        {
            Recv.Add(unchecked((short)0x0001), typeof(GSReqRegistServer));
            Recv.Add(unchecked((short)0x1001), typeof(GSResUserOnlineCount));


            Send.Add(typeof(LSReqUserOnlineCount), unchecked((short)0x0002));
        }
    }
}
