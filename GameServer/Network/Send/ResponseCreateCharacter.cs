
namespace GameServer.Network.Send
{
    public class ResponseCreateCharacter : ASendPacket
    {
        protected int result;

        public ResponseCreateCharacter(int res)
        {
            result = res;
        }

        protected internal override void Write()
        {
            WriteD(result);
        }
    }
}
