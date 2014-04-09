using GameServer.Network.Send;

namespace GameServer.Network.Recv
{
    public class RequestExit : ARecvPacket
    {
        protected internal override void Read()
        {
            
        }

        protected internal override void Run()
        {
            _Client.SendPacket(new ResponseExit());
        }
    }
}
