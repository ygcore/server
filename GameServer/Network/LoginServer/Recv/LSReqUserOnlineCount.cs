using Common.Model.Server;
using GameServer.Network.LoginServer.Send;
using ProtoBuf;
using System.Collections.Generic;
using System.IO;

namespace GameServer.Network.LoginServer.Recv
{
    public class LSReqUserOnlineCount : LSARecvPacket
    {
        protected List<ServerStruct> ServerList;
        protected internal override void Read()
        {
            int len = ReadH();
            using (var ms = new MemoryStream(ReadB(len)))
            {
                ServerList = Serializer.Deserialize<List<ServerStruct>>(ms);
            }
        }

        protected internal override void Run()
        {
            _Client.SendPacket(new GSResUserOnlineCount(ServerList));
        }
    }
}
