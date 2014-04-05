using LoginServer.Network.Send;
using LoginServer.Service;
using System.Linq;

namespace LoginServer.Network.Recv
{
    public class RequestSelectServer : ARecvPacket
    {
        protected int serverId;
        protected int channelId;
        protected internal override void Read()
        {
            serverId = ReadD();
            channelId = ReadD();
            ReadC();
        }

        protected internal override void Run()
        {
            var server = LoginServer.ServerList[serverId];

            if (server.IsUseAtKey && !_Client._Account.HasAKey)
                _Client.SendPacket(new ResponseLogin(AuthResponse.NoAtKey));
            else
                _Client.SendPacket(new ResponseSelectServer(serverId, channelId));
        }
    }
}
