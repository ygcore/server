using LoginServer.Network.GameServer;
using LoginServer.Network.GameServer.Send;
using LoginServer.Network.Send;

namespace LoginServer.Network.Recv
{
    public class RequestServerList : ARecvPacket
    {
        protected internal override void Read()
        {
            
        }

        protected internal override void Run()
        {
            // todo update user online count
            foreach (GSClient client in GSClientManager.GetInstance().GetAllGSClient())
                client.SendPacket(new LSReqUserOnlineCount());
            
            _Client.SendPacket(new ResponseServerList());
        }
    }
}
