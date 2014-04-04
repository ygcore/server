using LoginServer.Network.Recv;
using LoginServer.Network.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Network
{
    public class Opcode
    {
        public static Dictionary<short, Type> Recv = new Dictionary<short, Type>();
        public static Dictionary<Type, short> Send = new Dictionary<Type, short>();

        public static void Init()
        {
            Recv.Add(unchecked((short)0x8000), typeof(RequestLogin));
            Recv.Add(unchecked((short)0x8016), typeof(RequestServerList));


            Send.Add(typeof(ResponseLogin), unchecked((short)0x8001));
            Send.Add(typeof(ResponseServerList), unchecked((short)0x8017));
        }
    }
}
