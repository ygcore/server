
namespace GameServer.Network.Send
{
    public class ResponseRunning : ASendPacket
    {
        protected internal override void Write()
        {
            WriteD(1);
        }
    }
}
