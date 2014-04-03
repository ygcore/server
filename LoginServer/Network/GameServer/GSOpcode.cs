using LoginServer.Network.GameServer.Recv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Network.GameServer
{
    public class GSOpcode
    {
        public static Dictionary<short, Type> Recv = new Dictionary<short, Type>();
        public static Dictionary<Type, short> Send = new Dictionary<Type, short>();

        public static void Init()
        {
            Recv.Add(unchecked((short)0x0001), typeof(GRP_RequestRegistServer));
        }
    }
}
