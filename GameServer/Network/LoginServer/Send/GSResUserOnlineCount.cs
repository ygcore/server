using Common.Model.Server;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Network.LoginServer.Send
{
    public class GSResUserOnlineCount : LSASendPacket
    {
        protected List<ServerStruct> ServerList;

        public GSResUserOnlineCount(List<ServerStruct> list)
        {
            ServerList = list;

            // todo get channel online count assign to server list

        }

        protected internal override void Write()
        {
            byte[] buff;
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, ServerList);
                buff = ms.ToArray();
            }

            WriteH(buff.Length);
            WriteB(buff);
        }
    }
}
