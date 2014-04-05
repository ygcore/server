using Common.Model.Server;
using LoginServer.Service;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Network.GameServer.Recv
{
    public class GSResUserOnlineCount : GSARecvPacket
    {
        protected internal override void Read()
        {
            int serverId = ReadD();
            int count = ReadH();
            for(int i = 0; i < count; i++)
            {
                int channelId = ReadH();
                int online = ReadD();

                AuthService.GetInstance().UpdateChannelCurrentUserCount(serverId, channelId, online);
            }
        }

        protected internal override void Run()
        {
            
        }
    }
}
