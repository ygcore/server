using Common.Model.Server;
using GameServer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Network.LoginServer.Send
{
    public class GSReqRegistServer : LSASendPacket
    {
        protected internal override void Write()
        {
            WriteD(Configuration.Setting.ServerId);
            WriteS(Configuration.Setting.ServerName);
            WriteS(Configuration.Network.PublicIp);
            WriteC((Configuration.Setting.UseAKey) ? 1 : 0);
            WriteD(Configuration.GetInstance().Channels.Count);
            foreach(ChannelStruct channel in Configuration.GetInstance().Channels)
            {
                WriteH(channel.Id);
                WriteS(channel.Name);
                WriteH(channel.Port);
                WriteC(channel.Type);
                WriteD(channel.MaxUser);
                WriteD(channel.CurrentUser);
            }
        }
    }
}
