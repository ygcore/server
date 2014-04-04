using Common.Model.Server;
using GameServer.Network.LoginServer.Send;
using ProtoBuf;
using System.Collections.Generic;
using System.IO;

namespace GameServer.Network.LoginServer.Recv
{
    public class LSReqUserOnlineCount : LSARecvPacket
    {
        protected internal override void Read()
        {

        }

        protected internal override void Run()
        {
            _Client.SendPacket(new GSResUserOnlineCount());
        }
    }
}
