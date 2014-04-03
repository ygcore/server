using GameServer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Network.LoginServer.Send
{
    public class GSP_RegistServer : LSASendPacket
    {
        protected internal override void Write()
        {
            WriteD(Configuration.Setting.ServerId);
            WriteS(Configuration.Setting.ServerName);
            WriteS(Configuration.Network.PublicIp);
            WriteD(Configuration.GetInstance().Channels.Count);
            foreach(ChannelStruct channel in Configuration.GetInstance().Channels)
            {
                WriteS(channel.Name);
                WriteH(channel.Port);
                WriteC(channel.Type);
                WriteD(channel.MaxUser);
            }
        }
    }
}
