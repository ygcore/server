using GameServer.Network.LoginServer.Recv;
using GameServer.Network.LoginServer.Send;
using System;
using System.Collections.Generic;

namespace GameServer.Network.LoginServer
{
    public class LSOpcode
    {
        public static Dictionary<short, Type> Recv = new Dictionary<short, Type>();
        public static Dictionary<Type, short> Send = new Dictionary<Type, short>();

        public static void Init()
        {
            Recv.Add(unchecked((short)0x0002), typeof(LSReqUserOnlineCount));


            Send.Add(typeof(GSReqRegistServer), unchecked((short)0x0001));
            Send.Add(typeof(GSResUserOnlineCount), unchecked((short)0x1001));
        }
    }
}
