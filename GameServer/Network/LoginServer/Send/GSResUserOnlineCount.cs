using Common.Model.Server;
using Common.Utilities;
using GameServer.Config;
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
        public GSResUserOnlineCount()
        {

        }

        protected internal override void Write()
        {
            WriteD(Configuration.Setting.ServerId);
            WriteH(Configuration.GetInstance().Channels.Count);
            foreach (ChannelStruct channel in Configuration.GetInstance().Channels)
            {
                int online = ClientManager.GetInstance().GetUserOnlineCount(channel.Id);
                WriteH(channel.Id);
                WriteD(online);
            }
        }
    }
}
