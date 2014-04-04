using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Network.GameServer.Send
{
    public class LSReqUserOnlineCount : GSASendPacket
    {
        public LSReqUserOnlineCount()
        {

        }

        protected internal override void Write()
        {
            var data = LoginServer.ServerList;

            byte[] buff;
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, data);
                buff = ms.ToArray();
            }

            WriteB(buff);
        }
    }
}
