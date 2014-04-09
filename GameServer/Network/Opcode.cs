using GameServer.Network.Recv;
using GameServer.Network.Send;
using System;
using System.Collections.Generic;

namespace GameServer.Network
{
    public class Opcode
    {
        public static Dictionary<short, Type> Recv = new Dictionary<short, Type>();
        public static Dictionary<Type, short> Send = new Dictionary<Type, short>();

        public static void Init()
        {
            Recv.Add(unchecked((short)0x0001), typeof(RequestAuth));
            Recv.Add(unchecked((short)0x0010), typeof(RequestCharacterList));
            Recv.Add(unchecked((short)0x0014), typeof(RequestCreateCharacter));
            Recv.Add(unchecked((short)0x001E), typeof(RequestDeleteCharacter));
            Recv.Add(unchecked((short)0x0038), typeof(RequestCheckName));
            

            Send.Add(typeof(ResponseAuth), unchecked((short)0x0002));
            Send.Add(typeof(ResponseCharacterList), unchecked((short)0x0011));
            Send.Add(typeof(ResponseCreateCharacter), unchecked((short)0x0015));
            Send.Add(typeof(ResponseDeleteCharacter), unchecked((short)0x001F));
            Send.Add(typeof(ResponseCheckName), unchecked((short)0x0039));
        }
    }
}
