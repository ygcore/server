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
            Send.Add(typeof(GSP_RegistServer), unchecked((short)0x0001));
        }
    }
}
