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
            _Client.SendPacket(new ResponseServerList());
        }
    }
}
