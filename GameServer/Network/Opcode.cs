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
            Recv.Add(unchecked((short)0x0003), typeof(RequestExit));
            Recv.Add(unchecked((short)0x0005), typeof(RequestEntergame));
            Recv.Add(unchecked((short)0x0010), typeof(RequestCharacterList));
            Recv.Add(unchecked((short)0x0014), typeof(RequestCreateCharacter));
            Recv.Add(unchecked((short)0x001E), typeof(RequestDeleteCharacter));
            Recv.Add(unchecked((short)0x0038), typeof(RequestCheckName));
            Recv.Add(unchecked((short)0x003C), typeof(RequestUseSkill));
            Recv.Add(unchecked((short)0x008F), typeof(RequestInitGame));


            Send.Add(typeof(ResponseAuth), unchecked((short)0x0002));
            Send.Add(typeof(ResponseExit), unchecked((short)0x0004));
            Send.Add(typeof(ResponseRunning), unchecked((short)0x0006));
            Send.Add(typeof(ResponseCharacterList), unchecked((short)0x0011));
            Send.Add(typeof(ResponseCreateCharacter), unchecked((short)0x0015));
            Send.Add(typeof(ResponseDeleteCharacter), unchecked((short)0x001F));
            Send.Add(typeof(ResponseInitGame), unchecked((short)0x0020));
            Send.Add(typeof(ResponseCheckName), unchecked((short)0x0039));
            Send.Add(typeof(ResponseSetBuff), unchecked((short)0x003D));
            Send.Add(typeof(ResponseCharacterInfo), unchecked((short)0x0064));
            Send.Add(typeof(ResponseServerTime), unchecked((short)0x0080));
        }
    }
}
